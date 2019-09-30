using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;

namespace RS232DLL
{
    public class SPInstances<T> : List<SPInstance<T>> where T:SerialPort
    {

    }
    public class SPInstance<T>:SPInstanceBase<T> where T:SerialPort
    {
        public SPInstance(T sp) : base(sp) { }
        public SPInstance(T sp, Action<string, object> WHreader,bool IsHex=false) : base(sp, WHreader, IsHex)  { }
        public SPInstance(T sp, Action<byte[], object> WHreader) : base(sp, WHreader) { }
        public SPInstance(T sp, Action<string> reader, bool IsHex = false) : base(sp, reader, IsHex) { }
        public SPInstance(T sp, Action<byte[]> reader) : base(sp, reader) { }

    }
}
