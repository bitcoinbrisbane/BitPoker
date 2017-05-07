using System;
using ZeroMQ;

namespace BitPoker
{
    public delegate void MessageEventHandler(object sender, MessageArgs e);

    public class Server
    {
        public event MessageEventHandler MessageEvent;

        public void Listen(String name, UInt16 port = 5555)
        {
            using (var responder = new ZSocket(ZSocketType.REP))
            {
                // Bind
                responder.Bind(String.Format("tcp://*:{0})", port));
                OnMessageEvent(new MessageArgs() { Message = String.Format("Listing on tcp://*:{0}", port) });

                while (true)
                {
                    // Receive
                    using (ZFrame request = responder.ReceiveFrame())
                    {
                        OnMessageEvent(new MessageArgs() { Message = request.ReadString() });

                        // Send
                        responder.Send(new ZFrame(name));
                    }
                }
            }
        }

        protected void OnMessageEvent(MessageArgs e)
        {
            MessageEvent(this, e);
        }
    }
}
