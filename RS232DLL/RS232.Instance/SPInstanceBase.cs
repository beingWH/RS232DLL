using RS232DLL.Infra;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace RS232DLL
{
    public class SPInstanceBase :IDisposable
    {
        protected readonly WHSerialPort sp;
        protected IRS232 spm;

        public SPInstanceBase(string PortName,object accessoryData=null)
        {
            this.sp = new WHSerialPort(PortName,accessoryData);
        }
        public SPInstanceBase(string PortName, object accessoryData,PortConfig pc, Action<byte[], object> WHreader) :this(PortName,accessoryData)
        {
            this.SPInitialize(pc);
            this.BytesReaderInitialize(WHreader);
        }
        public SPInstanceBase(string PortName, object accessoryData, Action<byte[], object> WHreader) : this(PortName, accessoryData)
        {
            this.SPInitialize(null);
            this.BytesReaderInitialize(WHreader);
        }
        public SPInstanceBase(string PortName, Action<byte[], object> WHreader) : this(PortName)
        {
            this.SPInitialize(null);
            this.BytesReaderInitialize(WHreader);
        }
        public SPInstanceBase(string PortName, Action<string, object> WHreader) : this(PortName)
        {
            this.SPInitialize(null);
            this.StrReaderInitialize(WHreader);
        }
        public SPInstanceBase(string PortName,object accessoryData, Action<string, object> WHreader) : this(PortName, accessoryData)
        {
            this.SPInitialize(null);
            this.StrReaderInitialize(WHreader);
        }
        public SPInstanceBase(string PortName, object accessoryData,PortConfig pc, Action<string, object> WHreader) : this(PortName, accessoryData)
        {
            this.SPInitialize(pc);
            this.StrReaderInitialize(WHreader);

        }

        public SPInstanceBase(string PortName, Action<Hex, object> WHreader) : this(PortName)
        {
            this.SPInitialize(null);
            this.HexReaderInitialize(WHreader);
        }
        public SPInstanceBase(string PortName, object accessoryData, Action<Hex, object> WHreader) : this(PortName, accessoryData)
        {
            this.SPInitialize(null);
            this.HexReaderInitialize(WHreader);
        }
        public SPInstanceBase(string PortName, object accessoryData, PortConfig pc, Action<Hex, object> WHreader) : this(PortName, accessoryData)
        {
            this.SPInitialize(pc);
            this.HexReaderInitialize(WHreader);

        }




        public virtual void SPInitialize(PortConfig pc = null)
        {
            try
            {
                spm = RS232Factory.CreateClient(sp);
                if (pc == null)
                    spm.SetDefaultPortConfig();
                else
                    spm.SetPortConfig(pc);
            }
            catch { throw; }
        }
        public virtual void Dispose()
        {
            spm.Dispose();
        }

        public virtual void BytesReaderDispose(Action<byte[], object> reader)
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

        public virtual void BytesReaderInitialize(Action<byte[], object> reader)
        {
            try
            {
                if (reader != null)
                {
                    sp.DataReceived += spm.Sp_BytesReceived;
                    spm.WHBytesReader += reader;
                }
            }
            catch { throw; }
        }

        public virtual void HexReaderDispose(Action<Hex, object> reader)
        {
            try
            {
                if (spm != null)
                {
                    sp.DataReceived -= spm.Sp_HexReceived;
                    spm.WHHexReader -= reader;
                }
                sp.Close();
            }
            catch { throw; }
        }

        public virtual void HexReaderInitialize(Action<Hex, object> reader)
        {
            try
            {
                if (reader != null)
                {
                    sp.DataReceived += spm.Sp_HexReceived;
                    spm.WHHexReader += reader;
                }
            }
            catch { throw; }
        }

        public virtual void StrReaderInitialize(Action<string, object> reader)
        {
            try
            {
                if (reader != null)
                {
                    sp.DataReceived += spm.Sp_StrReceived;
                    spm.WHStrReader += reader;
                }
            }
            catch { throw; }
        }

        public virtual void StrReaderDispose(Action<string, object> reader)
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

        public virtual void WriteHex(Hex hex)
        {
            try
            {
                if (spm != null)
                    spm.WriteHex(hex);
            }
            catch { throw; }
        }

        public virtual void WriteStr(string str)
        {
            try
            {
                if (spm != null)
                    spm.WriteStr(str);
            }
            catch { throw; }
        }

    }
}
