using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS232DLL
{
    public interface IWHSPInstance
    {
        void ReaderInitialize(PortConfig pc, Action<string, object> reader);
        void ReaderInitialize(PortConfig pc, Action<byte[], object> reader);
        void ReaderDispose(Action<string, object> reader);
        void ReaderDispose(Action<byte[], object> reader);
        void Write(string str);

    }
}
