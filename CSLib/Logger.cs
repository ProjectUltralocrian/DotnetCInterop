using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSLib
{
    public class LogLevelEventArgs : EventArgs
    {
        public string Message { get; set; }
        public LogLevelEventArgs(string msg)
        {
            Message = msg;
        }
    }

    public class Logger : ILogger
    {
        [DllImport("CLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static void log(string message);

        public delegate void LogEventHandler(object sender, LogLevelEventArgs eventArgs);


        public event LogEventHandler? LogLevelChanged;

        public enum LogLevel
        {
            Debug, Info, Warning, Error, Fatal 
        }

        private LogLevel _level;

        public LogLevel Level
        {
            get { return _level; }
            set 
            {
                if (_level != value)
                {
                    LogLevelChanged?.Invoke(this, new LogLevelEventArgs("Loglevel changed to " + value));
                    _level = value;
                }
            }
        }

        public int MyProperty { get; set; }

        public void Warn(string message) 
        {
            Console.WriteLine("Calling C# library, which calls C function...");
            log(message);
        }

        public void Info(string message)
        {
            Console.WriteLine("[INFO] " + message);
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }
    }
}
