namespace UdpCastServiceTemp;

public class TempDevice
{
    public string Name { get; set; } = String.Empty;
    public string ID = new Guid().ToString();
    public string DeviceType { get; set; } = "Mobile";
    public DateTime LastSeen { get; set; } = DateTime.UtcNow;
}
