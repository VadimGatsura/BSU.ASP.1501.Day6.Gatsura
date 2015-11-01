using System;
using System.Diagnostics;
using NLog;

namespace Task1.BookListService.ConsoleTests {
    internal sealed class LoggingService {
        private static readonly Lazy<LoggingService> m_Instance = new Lazy<LoggingService>(() => new LoggingService());
        
        public static LoggingService Instance => m_Instance.Value;

        private LoggingService() {
            m_Logger = LogManager.GetCurrentClassLogger();
        }

        private readonly Logger m_Logger;

        public void Log(string message) { m_Logger.Info(message); }

        public void Log(Exception ex, string message = "") {
            if (Debugger.IsAttached)
                Debugger.Break();
            m_Logger.Error(ex, message);
        }
    }
}
