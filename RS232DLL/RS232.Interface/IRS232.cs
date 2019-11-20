using RS232DLL.Infra;
using System;
using System.IO.Ports;

namespace RS232DLL
{
    public interface IRS232:IDisposable
    {

        event Action<string, object> WHStrReader ;
        event Action<byte[], object> WHBytesReader;
        event Action<Hex, object> WHHexReader;


        string[] FindIO();
        void SetDefaultPortConfig();
        void SetPortConfig(PortConfig pc);
        void WriteStr(string str);
        void WriteHex(Hex str);
        void Sp_StrReceived(object sender, SerialDataReceivedEventArgs e);
        void Sp_HexReceived(object sender, SerialDataReceivedEventArgs e);
        void Sp_BytesReceived(object sender, SerialDataReceivedEventArgs e);
        void Close();
    }
}
