using System;
using System.Collections.Generic;
using System.Text;

namespace HermesLog.Model
{
    public class LogMessage
    {
        private string[] levels = { "Exception", "Error", "Warning", "Info", "Debug", "Verbose" };

        public int Level { get; set; }

        public string Module { get; set; }

        public string Action { get; set; }

        public string Message { get; set; }


        public long Timestamp { get; set; }
    }
}
