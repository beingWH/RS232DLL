using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS232DLL.Infra
{
     public static class RS232Utils
    {
        public static byte[] Hex2Bytes(string hex)
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
                    switch (s.Length)
                    {
                        case 1: result[i] = Byte.Parse(s, System.Globalization.NumberStyles.HexNumber);  break;
                        case 2: result[i] = Byte.Parse(s, System.Globalization.NumberStyles.HexNumber);  break;
                        case 3: result[i] = Byte.Parse(s.Remove(0,2), System.Globalization.NumberStyles.HexNumber); break;
                        case 4: result[i] = Byte.Parse(s.Remove(0, 2), System.Globalization.NumberStyles.HexNumber); break;
                    }
                    i++;
                }
                return result;
            }
            catch
            {
                throw;
            }

        }
        public static byte CheckSum(byte[] bytes)
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
        public static string NowTime2Str()
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
        public static string BytesTohexString(byte[] bytes)
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
    }
}
