using RS232DLL;
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
             SPInstance<WHSerialPort> spi =new SPInstance<WHSerialPort>();
            
            spi.StrReaderInitialize("COM10", (str,accessoryData) =>
            {
                Console.WriteLine(str);
            });
            for(int i = 0; i < 10; i++)
            {
                spi.WriteStr("FF\r\n");
                Thread.Sleep(100);
            }
            Console.ReadLine();

        }
       
    }
}
