using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace RS232DLL
{
    public sealed class WHSerialPort : SerialPort, IAccessoryData
    {
        public WHSerialPort(string Com,object data) : base(Com) {this.accessoryData = data; }
        public object accessoryData { get; set; }
    }
}
