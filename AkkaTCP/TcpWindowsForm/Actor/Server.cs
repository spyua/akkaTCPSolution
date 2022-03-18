using Akka.Actor;
using Akka.IO;
using AkkaSysBase;
using AkkaSysBase.Base;
using LogSender;

namespace TcpWindowsForm.Actor
{

    public class Server : BaseServerActor
    {
        public Server(SysIP sysIP, ILog log) : base(sysIP, log)
        {
            ReceiveAny(message => RcvObject(message));
        }

        private void RcvObject(object msg)
        {
            _log.E("RcvObject", $"VaildActor! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }
    }

}