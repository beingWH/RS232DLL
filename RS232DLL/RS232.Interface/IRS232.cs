using System;
using System.IO.Ports;

namespace RS232DLL
{
    public interface IRS232
    {

        event Action<string, object> WHStrReader ;
        event Action<byte[], object> WHBytesReader;
        event Action<string> StrReader;
        event Action<byte[]> BytesReader;

        string[] FindIO();
        void SetDefaultPortConfig();
        void SetPortConfig(PortConfig pc);
        void WriteStr(string str);
        void WriteHex(string str);
        void Sp_StrReceived(object sender, SerialDataReceivedEventArgs e);
        void Sp_HexReceived(object sender, SerialDataReceivedEventArgs e);
        void Sp_BytesReceived(object sender, SerialDataReceivedEventArgs e);
        void Close();
        void Dispose();
    }
}
