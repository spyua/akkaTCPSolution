using Akka.Actor;
using Akka.IO;
using AkkaSysBase.Def;
using LogSender;
using System;

namespace AkkaSysBase.Base
{
    public class BaseClientActor : BaseActor
    {
        private ICancelable _tryConnection;
        protected IActorRef _connection;

        private readonly SysIP _sysIp;
        private TCPDef.Statuts _tcpStatuts;


        public BaseClientActor(SysIP sysIp, ILog log) : base(log)
        {
            _sysIp = sysIp;
            _actorName = Context.Self.Path.Name;

            StarTryConnectionTmr();

            Receive<TCPDef.Cmd>(msg => ProEventCmd(msg));
            Receive<Tcp.Connected>(message => TCPConnected(message));
            Receive<Tcp.ConnectionClosed>(message => TcpConnectionClosed(message));
            Receive<Tcp.CommandFailed>(message => TcpCommandFailed(message));
            Receive<Tcp.Received>(message => TcpReceivedData(message));

        }
        protected override void PreRestart(Exception reason, object message)
        {
            base.PreRestart(reason, message);
            StopTryConnectionTmr();

        }

        private void ProEventCmd(TCPDef.Cmd msg)
        {
            switch (msg)
            {
                case TCPDef.Cmd.CLIENT_TRY_CONNECTION:
                    Connect();
                    break;
            }

        }


        protected virtual void TcpReceivedData(Tcp.Received msg)
        {
            _log.I("TCP Receive Data", "Handle_Tcp_Received. message=" + msg.ToString());
            _log.I("TCP Receive Data", "ByteString=" + msg.Data.ToString());
            _log.I("TCP Receive Data", "Count=" + msg.Data.Count.ToString());
        }
        protected virtual void TCPConnected(Tcp.Connected message)
        {
            _log.I("TCP Connect Suscess", " Connected. message=" + message.ToString());
            _log.I("TCP Connect Suscess", " LocalAddress=" + message.LocalAddress.ToString());
            _log.I("TCP Connect Suscess", " RemoteAddress=" + message.RemoteAddress.ToString());

            StopTryConnectionTmr();
            _tcpStatuts = TCPDef.Statuts.Connectiong;
            _connection = Sender;
            _connection.Tell(new Tcp.Register(Self));
        }
        protected virtual void TcpConnectionClosed(Tcp.ConnectionClosed message)
        {
            _log.I("TCP Connection Closed", " Tcp.ConnectionClosed. message=" + message.ToString());
            _log.I("TCP Connection Closed", " Message.Cause=" + message.Cause);
            _log.I("TCP Connection Closed", " Message.IsAborted=" + message.IsAborted.ToString());
            _log.I("TCP Connection Closed", " Message.IsConfirmed=" + message.IsConfirmed.ToString());
            _log.I("TCP Connection Closed", " Message.IsErrorClosed=" + message.IsErrorClosed.ToString());
            _log.I("TCP Connection Closed", " Message.IsPeerClosed=" + message.IsPeerClosed.ToString());
            _tcpStatuts = TCPDef.Statuts.Closed;
            StarTryConnectionTmr();
        }
        protected virtual void TcpCommandFailed(Tcp.CommandFailed message)
        {
            _log.E("TCP Operate Error", " Tcp.CommandFailed. message=" + message.ToString());
            _log.E("TCP Operate Error", " Tcp.CommandFailed. message=" + message.ToString());
            _log.E("TCP Operate Error", " Cmd=" + message.Cmd.ToString());
            _log.E("TCP Operate Error", " Message=" + message.Cmd.FailureMessage);
            _tcpStatuts = TCPDef.Statuts.Error;
        }

        private void StarTryConnectionTmr()
        {
            if (_tryConnection != null)
            {
                _log.A("已開啟嘗試連線", "已開啟嘗試連線");
                return;
            }

            _tcpStatuts = TCPDef.Statuts.Open;

            _log.I("開啟嘗試連線", "開啟嘗試連線");

            var interval = TimeSpan.FromSeconds(5);

            _tryConnection = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(interval, interval, Self, TCPDef.Cmd.CLIENT_TRY_CONNECTION, Self);

        }
        public void StopTryConnectionTmr()
        {
            _log.I("開啟嘗試連線關閉", "開啟嘗試連線關閉");


            if (_tryConnection != null)
            {
                _tryConnection.Cancel();
                _tryConnection = null;
            }

        }

        /// <summary>
        ///     連線
        /// </summary>
        protected void Connect()
        {
            if (_tcpStatuts == TCPDef.Statuts.Connectiong)
                return;

            _log.I("TCP連線操作", " TCP Connect");

            Sender?.Tell(Tcp.Close.Instance, Sender);
            _connection = null;
            Context.System.Tcp().Tell(new Tcp.Connect(_sysIp.RemoteIpEndPoint));

            _tcpStatuts = TCPDef.Statuts.Connectiong;

        }
    }
}
