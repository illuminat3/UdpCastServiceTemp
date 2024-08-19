using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpCastServiceTemp
{
    public class TempDevice
    {
        public string Name { get; set; } = String.Empty;
        public string ID = new Guid().ToString();
        public string DeviceType { get; set; } = "Mobile";
        public DateTime LastSeen { get; set; } = DateTime.UtcNow;
    }
}
