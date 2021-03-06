﻿using AkkaSysBase;
using AkkaSysBase.Base;
using LogSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpWindowsForm.Actor
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
