using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace UdpCastServiceTemp;
public class UdpCastProvider
{
    public const int port = 15555;
    private readonly UdpClient udpClient;

    public UdpCastProvider()
    {
        udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;
    }

    public void BroadcastMessage(object message)
    {
        try
        {
            string jsonMessage = JsonSerializer.Serialize(message);
            byte[] bytes = Encoding.UTF8.GetBytes(jsonMessage);

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, port);
            udpClient.Send(bytes, bytes.Length, endPoint);

            Console.WriteLine("Broadcast message sent: " + jsonMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error broadcasting message: " + ex.Message);
        }
    }

    public void Close()
    {
        udpClient.Close();
    }
}
