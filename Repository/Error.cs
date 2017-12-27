using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Error
    {
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
    }
}
