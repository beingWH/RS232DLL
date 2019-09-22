using RS232DLL.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS232DLL
{
    public class SPInstance<T> where T : WHSerialPort, new()
    {
        internal static T instance;
        internal static readonly object locker = new object();
        public static IRS232 spm;

        public SPInstance(){}
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new T();
                        }
                    }
                }
                return instance;
            }

        }
        public virtual void StrReaderInitialize(string portNme,Action<string,object> reader)
        {
            Instance.PortName = portNme;
            spm = RS232Factory.CreateClient(Instance);
            spm.SetDefaultPortConfig();
            Instance.DataReceived += spm.Sp_StrReceived;
            spm.StrReader += reader;
        }
        public virtual void HexReaderInitialize(string portNme, Action<string,object> reader)
        {
            Instance.PortName = portNme;
            spm = RS232Factory.CreateClient(Instance);
            spm.SetDefaultPortConfig();
            Instance.DataReceived += spm.Sp_HexReceived;
            spm.StrReader += reader;
        }
        public virtual void BytesReaderInitialize(string portNme, Action<byte[],object> reader)
        {
            Instance.PortName = portNme;
            spm = RS232Factory.CreateClient(Instance);
            spm.SetDefaultPortConfig();
            Instance.DataReceived += spm.Sp_BytesReceived;
            spm.BytesReader += reader;
        }
        public virtual void BytesReaderDispose(Action<byte[], object> reader)
        {
            Instance.DataReceived -= spm.Sp_BytesReceived;
            spm.BytesReader -= reader;
            Instance.Close();
        }
        public virtual void HexReaderDispose(Action<string, object> reader)
        {
            Instance.DataReceived -= spm.Sp_HexReceived;
            spm.StrReader -= reader;
            Instance.Close();
        }
        public virtual void StrReaderDispose(Action<string, object> reader)
        {
            Instance.DataReceived -= spm.Sp_StrReceived;
            spm.StrReader -= reader;
            Instance.Close();
        }
        public virtual void WriteStr(string str)
        {
            spm.WriteStr(str);
        }
        public virtual void WriteHex(string hex)
        {
            spm.WriteHex(hex);
        }
    }
}
