using RS232DLL;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SPInstances<SerialPort> SPIs = new SPInstances<SerialPort>()
            {
                new SPInstance<SerialPort>(new SerialPort("COM1"),delegate(string str){Console.WriteLine("COM1READER:"+str); }),
                new SPInstance<SerialPort>(new SerialPort("COM2"),delegate(string str){Console.WriteLine("COM2READER:"+str); }),
                new SPInstance<SerialPort>(new SerialPort("COM3"),delegate(string str){Console.WriteLine("COM3READER:"+str); }),
                new SPInstance<SerialPort>(new SerialPort("COM4"),delegate(string str){Console.WriteLine("COM4READER:"+str); }),
            };
            for(int i = 0; i < 10; i++)
            {
                SPIs[0].Write("this is com1");
                Thread.Sleep(500);
                SPIs[1].Write("this is com2");
                Thread.Sleep(500);
                SPIs[2].Write("this is com3");
                Thread.Sleep(500);
                SPIs[3].Write("this is com4");
                Thread.Sleep(500);
            }
            Console.Read();
        }
    }
}
