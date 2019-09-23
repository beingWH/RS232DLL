using System;

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
        public void StrReaderInitialize(string portNme,Action<string,object> reader)
        {
            Instance.PortName = portNme;
            spm = RS232Factory.CreateClient(Instance);
            spm.SetDefaultPortConfig();
            if (reader != null)
            {
                Instance.DataReceived += spm.Sp_StrReceived;
                spm.StrReader += reader;
            }
        }
        public void HexReaderInitialize(string portNme, Action<string,object> reader)
        {
            Instance.PortName = portNme;
            spm = RS232Factory.CreateClient(Instance);
            spm.SetDefaultPortConfig();
            if (reader != null)
            {
                Instance.DataReceived += spm.Sp_HexReceived;
                spm.StrReader += reader;
            }
        }
        public void BytesReaderInitialize(string portNme, Action<byte[],object> reader)
        {
            Instance.PortName = portNme;
            spm = RS232Factory.CreateClient(Instance);
            spm.SetDefaultPortConfig();
            if (reader != null)
            {
                Instance.DataReceived += spm.Sp_BytesReceived;
                spm.BytesReader += reader;
            }
        }
        public void BytesReaderDispose(Action<byte[], object> reader)
        {
            if (spm != null)
            {
                Instance.DataReceived -= spm.Sp_BytesReceived;
                spm.BytesReader -= reader;
            }
            Instance.Close();
        }
        public void HexReaderDispose(Action<string, object> reader)
        {
            if (spm != null)
            {
                Instance.DataReceived -= spm.Sp_HexReceived;
                spm.StrReader -= reader;
            }
            Instance.Close();
        }
        public void StrReaderDispose(Action<string, object> reader)
        {
            if (spm != null)
            {
                Instance.DataReceived -= spm.Sp_StrReceived;
                spm.StrReader -= reader;
            }

            Instance.Close();
        }
        public void WriteStr(string str)
        {
            if(spm!=null)
                spm.WriteStr(str);
        }
        public void WriteHex(string hex)
        {
            if(spm!=null)
                spm.WriteHex(hex);

        }

        public virtual void ReaderInitialize(PortConfig pc, Action<string, object> reader) { }
        public virtual void ReaderInitialize(PortConfig pc, Action<byte[], object> reader) { }
        public virtual void ReaderDispose(Action<string, object> reader) { }
        public virtual void ReaderDispose(Action<byte[], object> reader) { }
        public virtual void Write(string str) { this.WriteStr(str); }
    }
}
