using RS232DLL;
using RS232DLL.Infra;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Test
{
    public class SPInstance
    {
        internal static SPInstance instance;
        internal static readonly object locker = new object();
        internal SerialPort sp=new SerialPort();
        internal IRS232 spm;
        private SPInstance()
        {
        }
        public static SPInstance GetInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new SPInstance();
                    }
                }
            }
            return instance;
        }
        public void Initialize()
        {
            sp.PortName = "COM2";
            spm = RS232Factory.CreateClient(sp);
            spm.SetDefaultPortConfig();
            sp.DataReceived += spm.Sp_StrReceived;
            spm.SpReaderEvent += Spm_SpReaderEvent;
        }

        private void Spm_SpReaderEvent(string str)
        {
            Console.WriteLine(str);
        }

        public void Write(string str)
        {
            spm.WriteStr(str);
        }
    }
}
