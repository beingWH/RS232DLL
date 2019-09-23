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

        public object AccessoryData { get; set; }
    }
    public class PortConfig
    {
        public bool DrtEnable { get; set; }
        public bool RtsEnable { get; set; }
        public Handshake Handshake { get; set; }
        public int ReceivedBytesThreshold { get; set; }
        public int ReadBufferSize { get; set; }
        public int ReadTimeout { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits{get;set;}
        public Parity Parity { get; set; }
    }
}
