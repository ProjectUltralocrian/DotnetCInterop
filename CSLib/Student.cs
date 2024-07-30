using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSLib
{
    public class Student : IDisposable
    {
        public Logger _logger = new();
        public string Name { get; set; }

        public Student (string name) 
        {
            _logger.LogLevelChanged += OnLogLevelChanged;
            Name = name;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Console.WriteLine("Student disposed");
            _logger.LogLevelChanged -= OnLogLevelChanged;
        }

        public void OnLogLevelChanged(object sender, LogLevelEventArgs e)
        {
            Console.WriteLine(Name + ": " + sender + ", " + e.Message);
        }
    }
}
