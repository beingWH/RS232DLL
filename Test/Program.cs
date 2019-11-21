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

            using (MySpInstance COM2 = new MySpInstance("COM2"))
            {
                using (MySpInstance COM1 = new MySpInstance("COM1", "COM1 accessoryData"))
                {
                    COM1.BytesReaderInitialize((byts, obj) =>
                    {
                        Console.WriteLine($"{obj as string}:{byts.Count()}");
                    });

                    for (int i = 0; i < 10; i++)
                    {
                        COM2.WriteStr("This is COM2");
                        Thread.Sleep(200);
                    }
                    Console.Read();
                }
            }







            //List<SPInstanceBase> SPIs = new List<SPInstanceBase>()
            //{
            //    new SPInstanceBase("COM1","COM1 AccessoryData",delegate(Hex hex,object obj){Console.WriteLine((obj as string)+":"+hex.hexstring); }),
            //    new SPInstanceBase("COM2","COM2 AccessoryData",delegate(Hex hex,object obj){Console.WriteLine((obj as string)+":"+hex.hexstring); }),
            //    new SPInstanceBase("COM3","COM3 AccessoryData",delegate(Hex hex,object obj){Console.WriteLine((obj as string)+":"+hex.hexstring); }),
            //    new SPInstanceBase("COM4","COM4 AccessoryData",delegate(Hex hex,object obj){Console.WriteLine((obj as string)+":"+hex.hexstring); }),

            //};
            //for (int i = 0; i < 10; i++)
            //{
            //    SPIs[0].WriteHex("0x01 02 3 0x4".GetHexValue());
            //    Thread.Sleep(500);
            //    SPIs[1].WriteHex("0X05 06 7 0X8".GetHexValue());
            //    Thread.Sleep(500);
            //    SPIs[2].WriteHex("0x09 0A B 0xC".GetHexValue());
            //    Thread.Sleep(500);
            //    SPIs[3].WriteHex("0X0D 0E F 0x11".GetHexValue());
            //    Thread.Sleep(500);
            //}
            //Console.Read();





            //ISerialPort COM1 = RS232Helper.Create("COM1", "COM1 AccessoryData", (str, obj) =>
            //  {
            //      Console.Write($"{obj as string} : {str as string} \r\n ");
            //  });
            //ISerialPort COM2 = RS232Helper.Create("COM2", "COM2 AccessoryData", (str, obj) =>
            //{
            //    Console.Write($"{obj as string} : {str as string} \r\n ");
            //});
            //for (int i = 0; i < 10; i++)
            //{
            //    COM1.WriteStr("This is COM1");
            //    Thread.Sleep(500);
            //    COM2.WriteStr("This is COM2");
            //    Thread.Sleep(500);
            //}
            //Console.Read();
            //COM1.Dispose();
            //COM2.Dispose();

        }
    }
    public class MySpInstance : SPInstanceBase
    {
        public MySpInstance(string PortName, object accessoryData = null) : base(PortName, accessoryData)
        {
        }

        public MySpInstance(string PortName, Action<byte[], object> WHreader) : base(PortName, WHreader)
        {
        }

        public MySpInstance(string PortName, Action<string, object> WHreader) : base(PortName, WHreader)
        {
        }

        public MySpInstance(string PortName, Action<Hex, object> WHreader) : base(PortName, WHreader)
        {
        }

        public MySpInstance(string PortName, object accessoryData, Action<byte[], object> WHreader) : base(PortName, accessoryData, WHreader)
        {
        }

        public MySpInstance(string PortName, object accessoryData, Action<string, object> WHreader) : base(PortName, accessoryData, WHreader)
        {
        }

        public MySpInstance(string PortName, object accessoryData, Action<Hex, object> WHreader) : base(PortName, accessoryData, WHreader)
        {
        }

        public MySpInstance(string PortName, object accessoryData, PortConfig pc, Action<byte[], object> WHreader) : base(PortName, accessoryData, pc, WHreader)
        {
        }

        public MySpInstance(string PortName, object accessoryData, PortConfig pc, Action<string, object> WHreader) : base(PortName, accessoryData, pc, WHreader)
        {
        }

        public MySpInstance(string PortName, object accessoryData, PortConfig pc, Action<Hex, object> WHreader) : base(PortName, accessoryData, pc, WHreader)
        {
        }

        public override void BytesReaderDispose(Action<byte[], object> reader)
        {
            base.BytesReaderDispose(reader);
        }

        public override void BytesReaderInitialize(Action<byte[], object> reader)
        {
            base.BytesReaderInitialize(reader);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override void HexReaderDispose(Action<Hex, object> reader)
        {
            base.HexReaderDispose(reader);
        }

        public override void HexReaderInitialize(Action<Hex, object> reader)
        {
            base.HexReaderInitialize(reader);
        }

        public override void StrReaderDispose(Action<string, object> reader)
        {
            base.StrReaderDispose(reader);
        }

        public override void StrReaderInitialize(Action<string, object> reader)
        {
            base.StrReaderInitialize(reader);
        }

        public override void WriteHex(Hex hex)
        {
            base.WriteHex(hex);
        }

        public override void WriteStr(string str)
        {
            base.WriteStr(str);
        }
    }
}
