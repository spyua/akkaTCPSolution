using Akka.Actor;
using LogSender;
using System;

namespace AkkaSysBase.Base
{
    /// <summary>
    /// Base Actor
    /// </summary>
    public class BaseActor : ReceiveActor
    {
        protected ILog _log;
        protected string _actorName;

        public BaseActor(ILog log)
        {
            _log = log;
            _actorName = Context.Self.Path.Name;
        }

        protected override void PreStart()
        {
            //_log.I("AThread生命週期", _actorName + "PreStart");
            base.PreStart();

        }
        protected override void PreRestart(Exception reason, object message)
        {
            _log.E("AThread生命週期", _actorName + " PreRestart");
            _log.E("AThread生命週期", "Reason:" + reason.Message);
            base.PreRestart(reason, message);

        }
        protected override void PostStop()
        {
            _log.I("AThread生命週期", _actorName + " PostStop");
            base.PostStop();
        }
        protected override void PostRestart(Exception reason)
        {
            _log.E("AThread生命週期", _actorName + " PostRestart");
            _log.E("AThread生命週期", "Reason:" + reason.Message);
            base.PostRestart(reason);
        }

        /// <summary>
        /// Try Doing Action
        /// </summary>    
        protected void TryFlow(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                _log.E("TryFlow Expection", $"ex.Message={ex.Message}");
                _log.E("TryFlow Expection", $"ex.StackTrace={ex.StackTrace}");
            }
        }
    }
}
