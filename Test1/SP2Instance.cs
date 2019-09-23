using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using RS232DLL;

namespace ETMP.SPInstances
{
    public class SP2Instance
    {
        internal static SP2Instance instance;
        internal static readonly object locker = new object();
        public WHSerialPort sp = new WHSerialPort();
        public IRS232 spm;
        private SP2Instance()
        {
        }
        public static SP2Instance GetInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new SP2Instance();
                    }
                }
            }
            return instance;
        }
        public void Initialize(string PortName)
        {
            sp.PortName = PortName;
            spm = RS232Factory.CreateClient(sp);
            spm.SetDefaultPortConfig();
        }
        public void Write(string str)
        {
            spm.WriteStr(str);
        }

    }
}
