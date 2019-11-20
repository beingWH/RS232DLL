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
