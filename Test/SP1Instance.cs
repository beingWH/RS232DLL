using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using RS232DLL;

namespace ETMP.SPInstances
{
    public class SP1Instance
    {
        internal static SP1Instance instance;
        internal static readonly object locker = new object();
        public WHSerialPort sp = new WHSerialPort();
        public IRS232 spm;
        private SP1Instance()
        {
        }
        public static SP1Instance GetInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new SP1Instance();
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
        public void Close()
        {
            spm.Close();
        }
    }
}
