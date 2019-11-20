using System.IO.Ports;

namespace RS232DLL.Infra
{
    public static class RS232Factory
    {
        public static IRS232 CreateClient<T>(T sp) where T:SerialPort, IAccessoryData
        {
            RS232Methods<T> spm = new RS232Methods<T>(sp);
            return spm;
        }
    }
}
