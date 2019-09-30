using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace RS232DLL
{
   // public delegate void SpReaderDelegate(string str,object accessoryData);
    //public delegate void SpBytesReaderDelegate(byte[] bytes,object accessoryData);

    sealed class SPMeans<T>:IRS232 where T:SerialPort
    {
        private T sp;
        private bool m_IsTryToClosePort = false;
        private bool m_IsReceiving = false;
        public event Action<string, object> WHStrReader ;
        public event Action<byte[], object> WHBytesReader;
        public event Action<string> StrReader;
        public event Action<byte[]> BytesReader;
        public SPMeans(T sp)
        {
            this.sp = sp;
        }
        ~SPMeans()
        {
            if (sp.IsOpen)
            {
                sp.Close();
            }
        }
        /// <summary>
        /// 查找IO端口
        /// </summary>
        /// <returns></returns>
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
                sp.Close();
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
                sp.Close();
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
                    Thread.Sleep(500);
                    Type type = typeof(T);

                    if (type.Equals(typeof(WHSerialPort)))
                        WHStrReader(sp.ReadExisting(), ((WHSerialPort)sender).AccessoryData);
                    else
                        StrReader(sp.ReadExisting());
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
                    Thread.Sleep(100);
                    int n = sp.BytesToRead;
                    byte[] buf = new byte[n];
                    sp.Read(buf, 0, n);
                    Type type = typeof(T);
                    if (type.Equals(typeof(WHSerialPort)))
                        WHStrReader(BytesTohexString(buf), ((WHSerialPort)sender).AccessoryData);
                    else
                        StrReader(BytesTohexString(buf));
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
                    Thread.Sleep(1000);
                    int n = sp.BytesToRead;
                    byte[] buf = new byte[n];
                    sp.Read(buf, 0, n);
                    Type type = typeof(T);
                    if (type.Equals(typeof(WHSerialPort)))
                        WHBytesReader(buf, ((WHSerialPort)sender).AccessoryData);
                    else
                        BytesReader(buf);
                    sp.DiscardInBuffer();
                }
            }
            finally
            {
                m_IsReceiving = false;
            }

        }
        private string BytesTohexString(byte[] bytes)
        {
            if (bytes == null || bytes.Count() < 1)
            {
                return string.Empty;
            }

            var count = bytes.Count();

            var cache = new StringBuilder();
            for (int ii = 0; ii < count; ++ii)
            {
                cache.Append("0x");
                var tempHex = Convert.ToString(bytes[ii], 16).ToUpper();
                cache.Append(tempHex.Length == 1 ? "0" + tempHex : tempHex);
                cache.Append(" ");
            }

            return cache.ToString().Trim();
        }


        /// <summary>
        /// 写入IO
        /// </summary>
        public void WriteHex(string hex)
        {
            try
            {
                m_IsTryToClosePort = false;
                if (!sp.IsOpen)
                {
                    sp.Open();
                }
                byte[] bytes = Hex2Bytes(hex);
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
        /// <summary>
        /// 关闭IO
        /// </summary>
        /// <param name="sp"></param>
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
        public void Dispose()
        {
            this.Close();
            sp.Dispose();
        }
        private byte[] Hex2Bytes(string hex)
        {
            try
            {
                if (string.IsNullOrEmpty(hex))
                {
                    return new byte[0];
                }
                string[] str = hex.Split(' ');
                var re = new byte[str.Length];
                var result = new byte[re.Length];
                int i = 0;
                foreach (var s in str)
                {
                    var tempBytes = Byte.Parse(s.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
                    re[i] = tempBytes;
                    result[i] = tempBytes;
                    i++;
                }
                return result;
            }
            catch
            {
                throw;
            }

        }
        public byte CheckSum(byte[] bytes)
        {
            try
            {
                byte chksum = 0;
                for (int i = 0; i < bytes.Length; i++)
                {
                    chksum += bytes[i];
                }
                chksum = (byte)(~chksum + 1);
                return chksum;
            }
            catch
            {
                throw;
            }          
        }
        public string NowTime2Str()
        {
            try
            {
                string str = "";
                string TimeNum = DateTime.Now.ToLocalTime().ToString("yy/MM/dd/HH/mm/ss");
                string[] TimeSplit = TimeNum.Split('/');
                foreach (var s in TimeSplit)
                {
                    str += "0x" + s + " ";
                }
                string WeekNum = DateTime.Now.DayOfWeek.ToString();
                switch (WeekNum)
                {
                    case "Sunday": WeekNum = "0x00"; break;
                    case "Monday": WeekNum = "0x01"; break;
                    case "Tuesday": WeekNum = "0x02"; break;
                    case "Wednesday": WeekNum = "0x03"; break;
                    case "Thursday": WeekNum = "0x04"; break;
                    case "Friday": WeekNum = "0x05"; break;
                    case "Saturday": WeekNum = "0x06"; break;
                }
                str += WeekNum;
                return str;
            }
            catch
            {
                throw;
            }
         
        }



    }
}