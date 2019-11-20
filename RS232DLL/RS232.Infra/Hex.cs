using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RS232DLL
{
    public sealed class Hex
    {
        public Hex(string hexstring)
        {
            this.hexstring = hexstring;
        }
        public string hexstring { get; set; }
    }
    public static class HexExtension
    {
        public static Hex GetHexValue(this string hexstring)
        {
           
                Regex regex = new Regex(@"(?i)^((0x)?([a-f\d])+\s)*(0x)?([a-f\d])+$");
                Match match = regex.Match(hexstring);
                if (!match.Success)
                    throw new Exception("Hex字符串匹配失败！\r\nHex字符串规定格式为0x01 0x02或01 02");
                return new Hex(hexstring);
 


        }
    }

}
