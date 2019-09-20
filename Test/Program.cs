using RS232DLL.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SPInstance spi = SPInstance.GetInstance();
            spi.Initialize();
            for(int i=0; i < 100; i++)
            {
                spi.Write(i.ToString());
                Thread.Sleep(100);
            }
            spi.spm.Close();
            Console.ReadLine();
        }
    }
}
