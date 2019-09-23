using ETMP.SPInstances;
using RS232DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            SPInstance<WHSerialPort> sp2 = new SPInstance<WHSerialPort>();
            sp2.StrReaderInitialize("COM2", (str, accessoryData) =>
            {
                Console.WriteLine("[COM2 READER]" + str);
            });
            SPInstance<WHSerialPort> sp4 = new SPInstance<WHSerialPort>();
            sp4.StrReaderInitialize("COM4", (str, accessoryData) =>
            {
                Console.WriteLine("[COM4 READER]" + str);
            });
            for (int i = 0; i < 100; i++)
            {
                sp2.WriteStr(DateTime.Now + ": I am COM2\r\n");
                sp4.WriteStr(DateTime.Now + ": I am COM2\r\n");

                Thread.Sleep(1000);
            }
        }
    }
}
