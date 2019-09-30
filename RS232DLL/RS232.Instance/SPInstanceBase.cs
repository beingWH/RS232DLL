using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace RS232DLL
{
    public class SPInstanceBase<T> : IWHSPInstance where T:SerialPort
    {
        public T sp { get; set; }
        public IRS232 spm;
        public SPInstanceBase() { }
        public SPInstanceBase(T sp)
        {
            this.sp = sp;
        }
        public SPInstanceBase(T sp, Action<byte[], object> WHreader)
        {
            this.sp = sp;
            if(typeof(T).Equals(typeof(WHSerialPort)))
                this.BytesReaderInitialize(WHreader);
        }
        public SPInstanceBase(T sp, Action<string, object> WHreader,bool IsHex=false)
        {
            this.sp = sp;
            if (IsHex)
            {
                if (typeof(T).Equals(typeof(WHSerialPort)))
                    this.HexReaderInitialize(WHreader);
            }
            else
            {
                if (typeof(T).Equals(typeof(WHSerialPort)))
                    this.StrReaderInitialize(WHreader);
            }
        }
        public SPInstanceBase(T sp, Action<byte[]> reader)
        {
            this.sp = sp;
            if (!typeof(T).Equals(typeof(WHSerialPort)))
                this.BytesReaderInitialize(reader);
        }
        public SPInstanceBase(T sp, Action<string> reader, bool IsHex = false)
        {
            this.sp = sp;
            if (IsHex)
            {
                if (!typeof(T).Equals(typeof(WHSerialPort)))
                    this.HexReaderInitialize(reader);
            }
            else
            {
                if (!typeof(T).Equals(typeof(WHSerialPort)))
                    this.StrReaderInitialize(reader);
            }
        }

        public void BytesReaderDispose(Action<byte[], object> reader)
        {
            try
            {
                if (spm != null)
                {
                    sp.DataReceived -= spm.Sp_BytesReceived;
                    spm.WHBytesReader -= reader;
                }
                sp.Close();
            }
            catch { throw; }
        }

        public void BytesReaderInitialize(Action<byte[], object> reader)
        {
            try
            {
                spm = RS232Factory.CreateClient(sp);
                spm.SetDefaultPortConfig();
                if (reader != null)
                {
                    sp.DataReceived += spm.Sp_BytesReceived;
                    spm.WHBytesReader += reader;
                }
            }
            catch { throw; }
        }

        public void HexReaderDispose(Action<string, object> reader)
        {
            try
            {
                if (spm != null)
                {
                    sp.DataReceived -= spm.Sp_HexReceived;
                    spm.WHStrReader -= reader;
                }
                sp.Close();
            }
            catch { throw; }
        }

        public void HexReaderInitialize(Action<string, object> reader)
        {
            try
            {
                spm = RS232Factory.CreateClient(sp);
                spm.SetDefaultPortConfig();
                if (reader != null)
                {
                    sp.DataReceived += spm.Sp_HexReceived;
                    spm.WHStrReader += reader;
                }
            }
            catch { throw; }
        }

        public void StrReaderInitialize(Action<string, object> reader)
        {
            try
            {
                spm = RS232Factory.CreateClient(sp);
                spm.SetDefaultPortConfig();
                if (reader != null)
                {
                    sp.DataReceived += spm.Sp_StrReceived;
                    spm.WHStrReader += reader;
                }
            }
            catch { throw; }
        }

        public void StrReaderDispose(Action<string, object> reader)
        {
            try
            {
                if (spm != null)
                {
                    sp.DataReceived -= spm.Sp_StrReceived;
                    spm.WHStrReader -= reader;
                }

                sp.Close();
            }
            catch { throw; }
        }

        public void BytesReaderDispose(Action<byte[]> reader)
        {
            try
            {
                if (spm != null)
                {
                    sp.DataReceived -= spm.Sp_BytesReceived;
                    spm.BytesReader -= reader;
                }
                sp.Close();
            }
            catch { throw; }
        }

        public void BytesReaderInitialize(Action<byte[]> reader)
        {
            try
            {
                spm = RS232Factory.CreateClient(sp);
                spm.SetDefaultPortConfig();
                if (reader != null)
                {
                    sp.DataReceived += spm.Sp_BytesReceived;
                    spm.BytesReader += reader;
                }
            }
            catch { throw; }
        }

        public void HexReaderDispose(Action<string> reader)
        {
            try
            {
                if (spm != null)
                {
                    sp.DataReceived -= spm.Sp_HexReceived;
                    spm.StrReader -= reader;
                }
                sp.Close();
            }
            catch { throw; }
        }

        public void HexReaderInitialize(Action<string> reader)
        {
            try
            {
                spm = RS232Factory.CreateClient(sp);
                spm.SetDefaultPortConfig();
                if (reader != null)
                {
                    sp.DataReceived += spm.Sp_HexReceived;
                    spm.StrReader += reader;
                }
            }
            catch { throw; }
        }

        public void StrReaderInitialize(Action<string> reader)
        {
            try
            {
                spm = RS232Factory.CreateClient(sp);
                spm.SetDefaultPortConfig();
                if (reader != null)
                {
                    sp.DataReceived += spm.Sp_StrReceived;
                    spm.StrReader += reader;
                }
            }
            catch { throw; }
        }

        public void StrReaderDispose(Action<string> reader)
        {
            try
            {
                if (spm != null)
                {
                    sp.DataReceived -= spm.Sp_StrReceived;
                    spm.StrReader -= reader;
                }

                sp.Close();
            }
            catch { throw; }
        }



        public void WriteHex(string hex)
        {
            try
            {
                if (spm != null)
                    spm.WriteHex(hex);
            }
            catch { throw; }
        }

        public void WriteStr(string str)
        {
            try
            {
                if (spm != null)
                    spm.WriteStr(str);
            }
            catch { throw; }
        }

        public virtual void ReaderDispose(Action<string, object> reader) { this.StrReaderDispose(reader); }

        public virtual void ReaderDispose(Action<byte[], object> reader) { this.BytesReaderDispose(reader); }

        public virtual void ReaderInitialize(PortConfig pc, Action<string, object> reader) { this.StrReaderInitialize(reader); }

        public virtual void ReaderInitialize(PortConfig pc, Action<byte[], object> reader) { this.BytesReaderInitialize(reader); }

        public virtual void Write(string str)
        {
            this.WriteStr(str);
        }
    }
}
