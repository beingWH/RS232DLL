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
        internal WHSerialPort sp=new WHSerialPort();
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
            sp.AccessoryData = new { a = 1, b = 2 };
            sp.PortName = "COM10";
            spm = RS232Factory.CreateClient(sp);
            spm.SetDefaultPortConfig();
            sp.DataReceived += spm.Sp_StrReceived;
            spm.SpReaderEvent += Spm_SpReaderEvent;

        }

        private void Spm_SpReaderEvent(string str,Object obj)
        {
            dynamic n = obj;
            Console.WriteLine(str+"  "+n.a.ToString());
        }

        public void Write(string str)
        {
            spm.WriteStr(str);
        }
    }
}
