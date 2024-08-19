namespace UdpCastServiceTemp;

public static class Program
{

    public static UdpCastProvider udpCastProvider = new UdpCastProvider();
    public static UdpCastReceiver udpCastReceiver = new UdpCastReceiver();
    public static void Main(string[] args)
    {
        udpCastReceiver.StartListening();

        var tempDevice = new TempDevice()
        {
            Name = "Test",
            DeviceType = "Mobile",
            LastSeen = DateTime.Now,
        };

        udpCastProvider.BroadcastMessage(tempDevice);
    }
}