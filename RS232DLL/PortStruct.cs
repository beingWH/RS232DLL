using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS232DLL.Infra
{
    public struct PortStruct
    {
        public string PortName;
        public string Stop;
        public string Parity;
        public Int32 BaudRate;
        public Int32 DataBits;
    }
}