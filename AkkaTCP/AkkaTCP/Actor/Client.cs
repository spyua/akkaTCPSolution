using AkkaSysBase;
using AkkaSysBase.Base;
using LogSender;

namespace AkkaTCP.Actor
{
    public class Client : BaseClientActor
    {

      
        public Client(SysIP sysIP, ILog log) : base(sysIP, log)
        {
            ReceiveAny(message => RcvObject(message));

     
        }



        private void RcvObject(object msg)
        {
            _log.E("RcvObject", $"VaildActor! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }
    }
}
