using System;
using System.IO.Ports;
using System.Threading;

namespace RS232DLL.Infra
{
    sealed class RS232Methods<T>:IRS232 
        where T:SerialPort, IAccessoryData
    {
        private readonly T sp;
        private bool m_IsTryToClosePort = false;
        private bool m_IsReceiving = false;
        public event Action<string, object> WHStrReader ;
        public event Action<Hex, object> WHHexReader;
        public event Action<byte[], object> WHBytesReader;
        public RS232Methods(T sp)
        {
            this.sp = sp;
        }
        ~RS232Methods()
        {
            this.Close();
            sp.Dispose();
        }
        public void Dispose()
        {
            this.Close();
            sp.Dispose();
            GC.SuppressFinalize(this);
        }
        public string[] FindIO()
        {
            try
            {
                string[] str = SerialPort.GetPortNames();
                return str;
            }
            catch
            {
                throw;
            }
        }
        public void SetDefaultPortConfig()
        {
            try
            {
                if (sp.IsOpen)
                    sp.Close();
                sp.DtrEnable = true;
                sp.RtsEnable = true;
                sp.Handshake = Handshake.None;
                sp.ReceivedBytesThreshold = 1;
                sp.ReadBufferSize = 1000;
                sp.ReadTimeout = 1000;
                sp.BaudRate = 9600;
                sp.DataBits = 8;
                sp.StopBits = StopBits.One;
                sp.Parity = Parity.None;
                if (!sp.IsOpen)
                    sp.Open();
            }
            catch
            {
                throw;
            }     
        }
        public void SetPortConfig(PortConfig pc)
        {
            try
            {
                if (sp.IsOpen)
                    sp.Close();
                sp.DtrEnable = pc.DrtEnable;
                sp.RtsEnable = pc.RtsEnable;
                sp.Handshake =pc.Handshake;
                sp.ReceivedBytesThreshold = pc.ReceivedBytesThreshold;
                sp.ReadBufferSize = pc.ReadBufferSize;
                sp.ReadTimeout = pc.ReadTimeout;
                sp.BaudRate = pc.BaudRate;
                sp.DataBits = pc.DataBits;
                sp.StopBits = pc.StopBits;
                sp.Parity = pc.Parity;
                if (!sp.IsOpen)
                    sp.Open();
            }
            catch
            {
                throw;
            }
        }
        public void Sp_StrReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (m_IsTryToClosePort)
            {
                return;
            }
            m_IsReceiving = true;
            try
            {
                if (sp.IsOpen)
                {
                    //Thread.Sleep(100);
                    WHStrReader?.Invoke(sp.ReadExisting(), sp.accessoryData);
                    sp.DiscardInBuffer();
                    
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                m_IsReceiving = false;
            }
        }
        public void Sp_HexReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (m_IsTryToClosePort)
            {
                return;
            }
            m_IsReceiving = true;
            try
            {
                if (sp.IsOpen)
                {
                    //Thread.Sleep(100);
                    int n = sp.BytesToRead;
                    byte[] buf = new byte[n];
                    sp.Read(buf, 0, n);
                    WHHexReader?.Invoke(new Hex(RS232Utils.BytesTohexString(buf)), sp.accessoryData);
                    sp.DiscardInBuffer();
                }
            }
            finally
            {
                m_IsReceiving = false;
            }
        }
        public void Sp_BytesReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (m_IsTryToClosePort)
            {
                return;
            }
            m_IsReceiving = true;
            try
            {
                if (sp.IsOpen)
                {
                    //Thread.Sleep(100);
                    int n = sp.BytesToRead;
                    byte[] buf = new byte[n];
                    sp.Read(buf, 0, n);
                    WHBytesReader?.Invoke(buf, sp.accessoryData);
                    sp.DiscardInBuffer();
                }
            }
            finally
            {
                m_IsReceiving = false;
            }

        }
        public void WriteHex(Hex hex)
        {
            try
            {
                m_IsTryToClosePort = false;
                if (!sp.IsOpen)
                {
                    sp.Open();
                }
                byte[] bytes = RS232Utils.Hex2Bytes(hex.hexstring);
                sp.Write(bytes, 0, bytes.Length);
            }
            catch
            {
                throw;
            }

        }
        public void WriteStr(string str)
        {
            try
            {
                m_IsTryToClosePort = false;
                if (!sp.IsOpen)
                {
                    sp.Open();
                }
                sp.Write(str);
            }
            catch
            {
                throw;
            }
        }
        public void Close()
        {
            try
            {
                if (!m_IsReceiving)
                {
                    m_IsTryToClosePort = true;
                    if (sp.IsOpen)
                    {
                        sp.Close();
                    }
                }
            }
            catch
            {
                throw;
            }
        }






    }
}