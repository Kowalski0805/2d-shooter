using System;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    public class PingEvent : NetworkEvent
    {
        public PingEvent() { }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.PING };
            return data;
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.PING) throw new Exception("Invalid event type");

            return this;
        }
    }
}
