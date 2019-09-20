using RS232DLL.Infra;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS232DLL
{
    public static class RS232Factory
    {
        public static IRS232 CreateClient(WHSerialPort sp)
        {
            SPMeans spm = new SPMeans(sp);
            return spm;
        }
    }
}
