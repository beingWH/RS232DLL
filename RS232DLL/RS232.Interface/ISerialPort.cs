using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS232DLL
{
    public interface ISerialPort
    {
        void WriteStr(string str);
        void WriteHex(Hex hex);
        void Dispose();
    }
}
