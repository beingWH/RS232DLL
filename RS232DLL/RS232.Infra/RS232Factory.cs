using System.IO.Ports;

namespace RS232DLL
{
    public static class RS232Factory
    {
        public static IRS232 CreateClient<T>(T sp) where T:SerialPort
        {
            SPMeans<T> spm = new SPMeans<T>(sp);
            return spm;
        }
    }
}
