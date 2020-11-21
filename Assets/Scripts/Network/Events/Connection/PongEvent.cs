using System;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    public class PongEvent : NetworkEvent
    {
        public PongEvent() { }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.PONG };
            return data;
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.PONG) throw new Exception("Invalid event type");

            return this;
        }
    }
}
