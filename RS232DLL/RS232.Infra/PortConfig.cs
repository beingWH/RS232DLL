using System.IO.Ports;

namespace RS232DLL.Infra
{
    public class PortConfig
    {
        public string PortName { get; set; }
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
