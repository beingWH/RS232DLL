using RS232DLL;
using RS232DLL.Infra;
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
            List<SPInstanceBase> SPIs = new List<SPInstanceBase>()
            {
                new SPInstanceBase("COM1","1",delegate(Hex hex,object obj){Console.WriteLine((obj as string)+":"+hex.hexstring); }),
                new SPInstanceBase("COM2","2",delegate(Hex hex,object obj){Console.WriteLine((obj as string)+":"+hex.hexstring); }),

            };
            for (int i = 0; i < 10; i++)
            {
                SPIs[0].WriteHex("0x01 ab A 0xA".GetHexValue());
                Thread.Sleep(500);
                SPIs[1].WriteHex("0xAB 0XA B 1".GetHexValue());
                Thread.Sleep(500);
            }
            Console.Read();

        }
    }
}
