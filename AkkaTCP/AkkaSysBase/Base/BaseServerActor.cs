using Akka.Actor;
using Akka.IO;
using LogSender;
using System;

namespace AkkaSysBase.Base
{
    public class BaseServerActor : BaseActor
    {
        public BaseServerActor(SysIP sysIP, ILog log) : base(log)
        {
            Context.System.Tcp().Tell(new Tcp.Bind(Self, sysIP.LocalIpEndPoint));

            Receive<Tcp.Bound>(message => TcpBound(message));
            Receive<Tcp.Connected>(message => TCPConnected(message));
            Receive<Tcp.CommandFailed>(msg => TcpCommandFailed(msg));
            Receive<Tcp.ConnectionClosed>(message => TcpConnectionClosed(message));
            Receive<Tcp.Received>(message => TcpReceivedData(message));
        }

        protected override void PostStop()
        {
            base.PostStop();

            if (Self != null)
                Self.Tell(Tcp.Close.Instance);
        }

        protected override void PostRestart(Exception reason)
        {
            base.PostRestart(reason);

            if (Self != null)
                Self.Tell(Tcp.Close.Instance);
        }

        private void TcpBound(Tcp.Bound msg)
        {
            _log.I("TCP Listen", "Tcp.Bound Success. Listening on " + msg.LocalAddress);
        }

        protected virtual void TcpReceivedData(Tcp.Received msg)
        {
            _log.I("TCP Receive Data", "Handle_Tcp_Received. message=" + msg.ToString());
            _log.I("TCP Receive Data", "ByteString=" + msg.Data.ToString());
            _log.I("TCP Receive Data", "Count=" + msg.Data.Count.ToString());


        }
        protected virtual void TCPConnected(Tcp.Connected msg)
        {
            _log.I("TCP Connectioned", " Tcp.Connected. message=" + msg.ToString());
            _log.I("TCP Connectioned", " message.LocalAddress=" + msg.LocalAddress.ToString());
            _log.I("TCP Connectioned", " message.RemoteAddress=" + msg.RemoteAddress.ToString());
            Sender.Tell(new Tcp.Register(Self));

        }
        protected virtual void TcpCommandFailed(Tcp.CommandFailed message)
        {
            _log.E("TCP Operate Error", " Tcp.CommandFailed. message=" + message.ToString());
            _log.E("TCP Operate Error", " Tcp.CommandFailed. message=" + message.ToString());
            _log.E("TCP Operate Error", " Cmd=" + message.Cmd.ToString());
            _log.E("TCP Operate Error", " Message=" + message.Cmd.FailureMessage);
        }

        protected virtual void TcpConnectionClosed(Tcp.ConnectionClosed msg)
        {
            _log.I("TCP Connection Closed", " Tcp.ConnectionClosed. message=" + msg.ToString());
            _log.I("TCP Connection Closed", " Message.Cause=" + msg.Cause);
            _log.I("TCP Connection Closed", " Message.IsAborted=" + msg.IsAborted.ToString());
            _log.I("TCP Connection Closed", " Message.IsConfirmed=" + msg.IsConfirmed.ToString());
            _log.I("TCP Connection Closed", " Message.IsErrorClosed=" + msg.IsErrorClosed.ToString());
            _log.I("TCP Connection Closed", " Message.IsPeerClosed=" + msg.IsPeerClosed.ToString());
        }
      
    }
}
