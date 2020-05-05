using System.Device.Gpio;

namespace Api.Configuration
{
    public class DhtOptions
    {
        public bool Enabled { get; set; }
        public int PinNumber { get; set; }
        public PinNumberingScheme? PinNumberingScheme { get; set; }
    }
}