using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace RS232DLL
{
    public class WHSerialPort:SerialPort
    {

        public WHSerialPort() : base() { }

        public WHSerialPort(IContainer container) : base(container) { }

        public WHSerialPort(string portName) : base(portName) { }

        public WHSerialPort(string portName, int baudRate) : base(portName, baudRate) { }

        public WHSerialPort(string portName, int baudRate, Parity parity) : base(portName, baudRate, parity) { }

        public WHSerialPort(string portName, int baudRate, Parity parity, int dataBits) : base(portName, baudRate, parity, dataBits) { }

        public WHSerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits):base(portName, baudRate, parity, dataBits, stopBits) { }

        public Object AccessoryData { get; set; }
    }
}
