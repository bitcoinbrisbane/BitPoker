#define _CRT_SECURE_NO_WARNINGS

#include <windows.h>
#include <cstdio>
#include <cstring>

//optional, used to whiten output using Von Newmann's algorithm.
class VonNeumann {
   size_t bitCount_; //how many generated so far
   size_t size_; //in bits
   unsigned char* to_; //external buffer
public:

   //specify the (external) buffer and buffer size we will fill.
   VonNeumann(unsigned char* to, const size_t size) : bitCount_(0), size_(8U * size), to_(to)
   {
      memset(to, 0, size); //start all 0's
   }

   //We add bits from right to left, because it's less fuzzing and since the
   // data is random, it doesn't matter.
   //true if we filled he buffer specified in the constructor. false if you need
   // to call here again.
   bool add(const unsigned char* from, const size_t count)
   {
      for (const unsigned char* const fence = from + count; from < fence;)
         for (unsigned char v = *from++; v; v >>= 2) //stop on all 0 because 0 doesn't contribute.
            switch (v & 3)
            {
               //discard 0 and 3
            case 2: //1,0 => 1
               to_[bitCount_ >> 3] |= 1 << (bitCount_ & 7);
               //fall
            case 1: //0,1 => 0 which is already in place
               if (++bitCount_ >= size_)
                  return true; //we're full!
            }
      return false; //we need more
   }
};


/* This supports a TrueRNG random number generation device. See
http://ubld.it/products/truerng-hardware-random-number-generator/
It operates like a Serial port device; pass in the COM port number
when constructing the object. When construction is done, .bad is true
if something didn't work, false if all is well. Then call
.getByte() for each 8 bits of random data you want. On an error,
bad is set true and getByte() will start returning 0's.

This was tested on Windows 7 and Windows XP, but should work on any Windows
version since XP.

We turns\ off the transmitter when the buffer is not being filled.
This may save a mA or two and also some CPU.

This is a single buffered approach; if you need data at the max possible
rate, consider implementing double buffering.

Bytes are not returned in the order generated, but since they are random
that shouldn't matter. If testing for patterns, consider changing getByte()
to return from first to last.

A bug in Windows 7 or the trueRNG driver causes ReadFile to hang if
the USB device is pulled out. It seems to hang regardless of the comm timeout
settings. Plan to recode with threads and timers if you need to work around
this.

If the device breaks and starts returning all 0's or all 1'a, getWhiterByte()
will become an infinite loop.

This is sample code; change it to account for your tolerance for errors.
No copyright, no warranties, etc. If it does what you need, yay.
*/
class RandomFromTrueRNG {
   unsigned char buf[2048]; //Buffer of randomness. Any smallish positive size will do.
   //Values in the megabytes range are probably a mistake and may run getWhiterByte() into
   // stack size problems.

   int at; //read pointer into buffer
   HANDLE h_; //COM handle
public:

   bool bad; //false is all is well

   RandomFromTrueRNG(unsigned int port) //com port, generally 1 to 24 or so.
   {
      bad = true; //not set up yet
      h_ = INVALID_HANDLE_VALUE; //no handle yet
      at = 0; //no bits yet

      char comname[64];
      sprintf_s(comname, "\\\\.\\COM%u", port); // Create \\.\COM7, which is how CreateFileA likes it.

      HANDLE hPort = CreateFileA(
         comname, GENERIC_READ | GENERIC_WRITE,
         0, NULL, OPEN_EXISTING, 0, NULL);
      if (hPort == INVALID_HANDLE_VALUE)
      {
         printf("Open failed, %d\n", GetLastError());
         return;
      }

      BOOL res;
      DWORD word;
      ClearCommError(hPort, &word, NULL);
      DCB PortDCB;

      // Initialize the DCBlength member.
      PortDCB.DCBlength = sizeof (DCB);

      // Get the default port setting information.
      GetCommState(hPort, &PortDCB);

      // Change the DCB structure settings.
      PortDCB.BaudRate = 57600;             // Any legal value will do; it's not used.
      PortDCB.fBinary = TRUE;               // Binary mode; no EOF check
      PortDCB.fParity = FALSE;              // parity checking
      PortDCB.fOutxCtsFlow = FALSE;         // No CTS output flow control
      PortDCB.fOutxDsrFlow = FALSE;         // No DSR output flow control
      PortDCB.fDtrControl = DTR_CONTROL_ENABLE; // DTR flow control type, must be on
      PortDCB.fDsrSensitivity = FALSE;      // DSR sensitivity
      PortDCB.fTXContinueOnXoff = TRUE;     // XOFF continues Tx
      PortDCB.fOutX = FALSE;                // No XON/XOFF out flow control
      PortDCB.fInX = FALSE;                 // No XON/XOFF in flow control
      PortDCB.fErrorChar = FALSE;           // Disable error replacement
      PortDCB.fNull = FALSE;                // Disable null stripping
      PortDCB.fRtsControl = RTS_CONTROL_ENABLE;    // RTS flow control
      PortDCB.fAbortOnError = FALSE;        // Do not abort reads/writes on error
      //these should have no effect, so we use typical values
      PortDCB.ByteSize = 8;                 // 8
      PortDCB.Parity = (BYTE)0;             // N
      PortDCB.StopBits = ONESTOPBIT;        // 1

      // Configure the port according to the specifications of the DCB
      // structure.
      if (!SetCommState(hPort, &PortDCB))
      {
         CloseHandle(hPort);
         printf("SetCommState failed\n");
         return ;
      }
      COMMTIMEOUTS timeout;
      memset(&timeout, 0, sizeof timeout);
      timeout.ReadIntervalTimeout = 200;
      timeout.ReadTotalTimeoutMultiplier = 1;
      timeout.ReadTotalTimeoutConstant = 1; //0; //readtimeoutms;
      res = SetCommTimeouts(hPort, &timeout);
      if (!res)
      {
         printf("SetCommTimeouts failed\n");
         CloseHandle(hPort);
         return ;
      }
      //good to go
      h_ = hPort;
      bad = false;
      //optionally, call getByte() or getWhiterByte() here, to fill the buffer
      // in advance and discard the first result. Recommended to make sure
      // everything is working, but not essential.
      getWhiterByte();
   }

   //returns 0 on error, else number of bytes received
   int fill(unsigned char* b, size_t s)
   {
      //Turn on DTR, which lets the trueRNG device transmit data. By preventing
      // transmission when we don't need data, we save an mA or two and maybe
      // some CPU.
      if (h_ == INVALID_HANDLE_VALUE || EscapeCommFunction(h_, SETDTR) == 0)
      {
         bad = true;
         return 0;
      }

      int iat = 0;
      //coded so it will still work if reading is done without blocking; but the intent
      // of the timeout settings is that we fill the buffer in one read.
      for (int wanted = (int)s; wanted > 0;)
      {
         DWORD result = 0;
         if (!ReadFile(h_, b + iat, (DWORD)wanted, &result, NULL))
         {
            //failed. We have no data.
            bad = true;
            iat = 0;
            break;
         }
         if (result == 0) //should not arise if using blocking
         {
            Sleep(10); //give it more time? More likely an error.
         }
         else //keep track of what we have
         {
            iat += result;
            wanted -= result;
         }
      }

      //All done. Stop transmitting.
      EscapeCommFunction(h_, CLRDTR);

      printf("Got %d bytes from trueRNG\n", iat);
      return iat;
   }

   //On error, we return 0 and .bad is set true.
   //Otherwise, 8 random bits.
   //If you want extra whitening, use the next fuction instead.
   unsigned char getByte()
   {
      if (--at < 0)
      {
         //need to refill
         at = fill(buf, sizeof buf);
         if (--at < 0)
            return 0; //start returning 0's; we're broken.
      }
      return buf[at];
   }

   //On error, we return 0 and .bad is set true.
   //Otherwise, 8 random bits, with extra whitening.
   //This whitens the output using Von Neumann's algorithm. This is very likely
   // unnecessary - the device already does whitening. But if you have a driving
   // need to claim you've gone the extra mile to be unbiased, and don't mind the
   // considerable loss of speed this costs, this does a good job.
   //Note: if the device breaks and generates all 0's or all 1's, this function
   // will never return.
   unsigned char getWhiterByte()
   {
      if (--at < 0)
      {
         //Von Neumann throws away a good number of bits, so we'll need
         //to fetch a few times to get enough.
         unsigned char tmp[32 + (sizeof buf)];
         for (VonNeumann whiter(buf, sizeof buf);;)
         {
            //get some randomness
            int c = fill(tmp, sizeof tmp);
            if (c == 0)
               return 0; //Sorry, no data
            //distill until the output buffer is full
            if (whiter.add(tmp, c))
               break; //got enough
            //need more
         }
         at = (sizeof buf) - 1;
      }
      //printf("%02x ", buf[at]);
      return buf[at];
   }

   ~RandomFromTrueRNG()
   {
      bad = true;
      CloseHandle(h_);
      h_ = INVALID_HANDLE_VALUE;
      at = 0;
   }
};

//typical use:
//RandomFromTrueRNG rndFromTRNG(7); //com port 7
//unsigned char randomByte = rndFromTRNG.getByte();
//...
