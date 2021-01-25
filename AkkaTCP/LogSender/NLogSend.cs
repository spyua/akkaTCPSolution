using Akka.Event;

namespace LogSender
{
    public class NLogSend : ILog
    {
        private ILoggingAdapter _nlog;       // Nlog

        public NLogSend(ILoggingAdapter nlog = null)
        {
            _nlog = nlog;
        }

        public void A(string title, string contetnt)
        {
            _nlog.Warning("【" + title + "】" + ":" + contetnt);
        }

        public void D(string title, string content)
        {
            _nlog.Debug("【" + title + "】" + ":" + content);
        }

        public void E(string title, string content)
        {
            _nlog.Error("【" + title + "】" + ":" + content);
        }

        public void I(string title, string content)
        {
            _nlog.Info("【" + title + "】" + ":" + content);
        }
    }
}
