namespace RS232DLL
{
    public static class RS232Factory
    {
        public static IRS232 CreateClient(WHSerialPort sp)
        {
            SPMeans spm = new SPMeans(sp);
            return spm;
        }
    }
}
