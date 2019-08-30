using RS232DLL.Infra;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace RS232DLL
{
    public interface IRS232
    {
        event SpReaderDelegate SpReaderEvent;
        event SpBytesReaderDelegate SpBytesReaderEvent;
        string[] FindIO();
        void SetDefaultPortConfig();
        void WriteStr(string str);
        void WriteHex(string str);
        void Sp_StrReceived(object sender, SerialDataReceivedEventArgs e);
        void Sp_HexReceived(object sender, SerialDataReceivedEventArgs e);
        void Sp_BytesReceived(object sender, SerialDataReceivedEventArgs e);
        void Close();
        void Dispose();
    }
}
