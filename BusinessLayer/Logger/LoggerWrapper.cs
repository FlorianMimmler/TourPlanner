using BusinessLayer.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logger
{
    public class LoggerWrapper: ILoggerWrapper
    {
        private readonly ILog _log;

        public LoggerWrapper(Type type)
        {
            _log = LogManager.GetLogger(type);
        }

        public void Debug(string message)
        {
            _log.Debug(message);
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Fatal(string message)
        {
            _log.Fatal(message);
        }

        public void Warn(string message)
        {
            _log.Warn(message);
        }
    }
}
