using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace UdpCastServiceTemp;

public class UdpCastReceiver
{
    private const int Port = 12121;
    private readonly UdpClient udpClient;
    private readonly List<TempDevice> devices;
    private readonly CancellationTokenSource cancellationTokenSource;

    public UdpCastReceiver()
    {
        udpClient = new UdpClient(Port);
        devices = new List<TempDevice>();
        cancellationTokenSource = new CancellationTokenSource();
        Task.Run(() => Cleanup(cancellationTokenSource.Token));
    }

    public void StartListening()
    {
        while (true)
        {
            try
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, Port);
                byte[] receiveBytes = udpClient.Receive(ref remoteEndPoint);
                string receivedData = Encoding.UTF8.GetString(receiveBytes);

                TempDevice? tempDevice = JsonSerializer.Deserialize<TempDevice>(receivedData);

                if (tempDevice != null)
                {
                    lock (devices)
                    {
                        var existingDevice = devices.FirstOrDefault(d => d.ID == tempDevice.ID);
                        if (existingDevice != null)
                        {
                            existingDevice.LastSeen = DateTime.UtcNow;
                        }
                        else
                        {
                            devices.Add(tempDevice);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    public List<TempDevice> GetDevices()
    {
        lock (devices)
        {
            return devices.ToList();
        }
    }

    private void Cleanup(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            lock (devices)
            {
                devices.RemoveAll(d => (DateTime.UtcNow - d.LastSeen).TotalMinutes > 2);
            }
            Thread.Sleep(30000);
        }
    }

    public void StopListening()
    {
        cancellationTokenSource.Cancel();
        udpClient.Close();
    }
}
