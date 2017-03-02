using System;
using System.Runtime.InteropServices;

namespace TrueRNGWrapper
{
    public class Wrapper : BitPoker.Models.IRandom
    {
        [DllImport("Win32Random.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CreateRng(int port);

        [DllImport("Win32Random.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Fill(byte[] b, int size);

        [DllImport("Win32Random.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteRng();

        private readonly Int32 _port;

        public Wrapper(Int32 port)
        {
            _port = port;
        }

        public int Next()
        {
            CreateRng(_port);

            Byte[] randombytes = new Byte[52];

            int nbytes = Fill(randombytes, 52);

            DeleteRng();

            return nbytes;
        }
    }
}
