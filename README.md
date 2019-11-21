# RS232DLL

## < Version 1.1.0
### How to Use

1. New Instance
```
  SPInstance<WHSerialPort> sp = new SPInstance<WHSerialPort>();
```
2. New Reader
```
  sp.StrReaderInitialize("COM1", (str, accessoryData) =>
  {
      Console.WriteLine("[COM1 READER]" + str);
  });
```
  You can Use 3 Reader Types:
  - StrReader
  - BytesReader
  - HexReader

3. Write something
```
  sp.Write("something");
  sp.WriteStr("something");
  sp.WriteHex("0xFF 0x10");
```
4. Dispose Reader
  If you no longer needed to use Reader,you can dispose reader like this.
```
  sp.StrReaderDispose((str, accessoryData) =>
  {
      Console.WriteLine("[COM1 READER]" + str);
  });
```

### How to Customize your own methods

You can override those methods.
```
    public virtual void ReaderInitialize(PortConfig pc, Action<string, object> reader) { }
    public virtual void ReaderInitialize(PortConfig pc, Action<byte[], object> reader) { }
    public virtual void ReaderDispose(Action<string, object> reader) { }
    public virtual void ReaderDispose(Action<byte[], object> reader) { }
    public virtual void Write(string str) { this.WriteStr(str); }
```
Below is a case.
```
    public class MyRS232 : SPInstance<WHSerialPort>
    {
        public override void ReaderInitialize(PortConfig pc, Action<string, object> reader)
        {
            try
            {
                Instance.PortName = pc.PortName;
                //put accessorydata to instance
                Instance.AccessoryData = "some data";
                spm = RS232Factory.CreateClient(Instance);
                spm.SetPortConfig(pc);
                if (reader != null)
                {
                    Instance.DataReceived += spm.Sp_StrReceived;
                    spm.StrReader += reader;
                }
            }
            catch { throw; }
        }
    }
```

##  Version 1.1.0

```
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
```

##  Version 2.0.0
### Quick to Start

COM1&Com2 is the pair of SerialPort. 

```
  ISerialPort COM1 = RS232Helper.Create("COM1", "COM1 AccessoryData", (str, obj) =>
    {
        Console.Write($"{obj as string} : {str as string} \r\n ");
    });
  ISerialPort COM2 = RS232Helper.Create("COM2", "COM2 AccessoryData", (str, obj) =>
  {
      Console.Write($"{obj as string} : {str as string} \r\n ");
  });
  for (int i = 0; i < 10; i++)
  {
      COM1.WriteStr("This is COM1");
      Thread.Sleep(500);
      COM2.WriteStr("This is COM2");
      Thread.Sleep(500);
  }
  Console.Read();
  COM1.Dispose();
  COM2.Dispose();
```

Result:

### SerialPort Group

pair of SerialPort:COM1&COM2,COM3&COM4

```
  List<SPInstanceBase> SPIs = new List<SPInstanceBase>()
  {
      new SPInstanceBase("COM1","COM1 AccessoryData",delegate(Hex hex,object obj){Console.WriteLine((obj as string)+":"+hex.hexstring); }),
      new SPInstanceBase("COM2","COM2 AccessoryData",delegate(Hex hex,object obj){Console.WriteLine((obj as string)+":"+hex.hexstring); }),
      new SPInstanceBase("COM3","COM3 AccessoryData",delegate(Hex hex,object obj){Console.WriteLine((obj as string)+":"+hex.hexstring); }),
      new SPInstanceBase("COM4","COM4 AccessoryData",delegate(Hex hex,object obj){Console.WriteLine((obj as string)+":"+hex.hexstring); }),

  };
  for (int i = 0; i < 10; i++)
  {
      SPIs[0].WriteHex("0x01 02 3 0x4".GetHexValue());
      Thread.Sleep(500);
      SPIs[1].WriteHex("0X05 06 7 0X8".GetHexValue());
      Thread.Sleep(500);
      SPIs[2].WriteHex("0x09 0A B 0xC".GetHexValue());
      Thread.Sleep(500);
      SPIs[3].WriteHex("0X0D 0E F 0x11".GetHexValue());
      Thread.Sleep(500);
  }
  Console.Read();
```
Result:

### Customize your own Class

pair of SerialPort:COM1&COM2

```
 public class MySpInstance : SPInstanceBase
    {
    // override virtual methods
    }
```
u can use your serialport class,like this

```
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
```
Result:
