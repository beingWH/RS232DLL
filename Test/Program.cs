using RS232DLL;
using System;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SPInstance<WHSerialPort> sp1 = new SPInstance<WHSerialPort>();
            sp1.StrReaderInitialize("COM1", (str, accessoryData) =>
            {
                Console.WriteLine("[COM1 READER]" + str);
            });
            SPInstance<WHSerialPort> sp3 = new SPInstance<WHSerialPort>();
            sp3.StrReaderInitialize("COM3", (str, accessoryData) =>
            {
                Console.WriteLine("[COM3 READER]" + str);
            });
            for (int i = 0; i < 100; i++)
            {
                sp1.WriteStr(DateTime.Now + ": I am COM1\r\n");
                sp3.WriteStr(DateTime.Now + ": I am COM3\r\n");

                Thread.Sleep(1000);
            }

        }

    }
}
