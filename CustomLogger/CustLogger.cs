using NLog;

namespace CustomLogger
{
    public sealed class CustLogger : ILogger
    {
        private static readonly Logger Nlogger = LogManager.GetCurrentClassLogger();

        static CustLogger() { }

        private CustLogger() { }

        public static CustLogger GetCurrentClassLogger { get; } = new CustLogger();

        #region ILogger

        public void Trace(string message) => Nlogger.Trace(message);

        public void Debug(string message) => Nlogger.Debug(message);

        public void Info(string message) => Nlogger.Info(message);

        public void Warn(string message) => Nlogger.Warn(message);

        public void Error(string message) => Nlogger.Error(message);

        public void Fatal(string message) => Nlogger.Fatal(message);

        #endregion
    }
}
