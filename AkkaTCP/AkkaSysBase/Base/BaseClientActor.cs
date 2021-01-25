using Akka.Actor;
using Akka.IO;
using LogSender;

namespace AkkaSysBase.Base
{
    public class BaseClientActor : BaseActor
    {

        protected IActorRef _connection;
        private readonly SysIP _sysIp;

        public BaseClientActor(SysIP sysIp, ILog log) : base(log)
        {
            _sysIp = sysIp;
            _actorName = Context.Self.Path.Name;

            Connect();

            Receive<Tcp.Connected>(message => TCPConnected(message));
            Receive<Tcp.ConnectionClosed>(message => TcpConnectionClosed(message));
            Receive<Tcp.CommandFailed>(message => TcpCommandFailed(message));
            Receive<Tcp.Received>(message => TcpReceivedData(message));

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
            Connect();
        }
        protected virtual void TcpCommandFailed(Tcp.CommandFailed message)
        {
            _log.E("TCP Operate Error", " Tcp.CommandFailed. message=" + message.ToString());
            _log.E("TCP Operate Error", " Tcp.CommandFailed. message=" + message.ToString());
            _log.E("TCP Operate Error", " Cmd=" + message.Cmd.ToString());
            _log.E("TCP Operate Error", " Message=" + message.Cmd.FailureMessage);
            Connect();
        }


        protected void Connect()
        {
            Context.System.Tcp().Tell(new Tcp.Connect(_sysIp.RemoteIpEndPoint));
        }
    }
}
