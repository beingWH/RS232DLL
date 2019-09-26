# RS232DLL

## How to Use

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

## How to Customize your own methods

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
