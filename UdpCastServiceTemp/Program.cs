namespace UdpCastServiceTemp;

public static class Program
{

    public static UdpCastProvider udpCastProvider = new UdpCastProvider();
    public static void Main(string[] args)
    {
        var tempDevice = new TempDevice()
        {
            Name = "Test",
            DeviceType = "Mobile",
            LastSeen = DateTime.Now,
        };

        udpCastProvider.BroadcastMessage(tempDevice);
    }
}