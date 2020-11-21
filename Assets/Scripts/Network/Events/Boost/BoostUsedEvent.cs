using System;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    // TODO: implement
    public class BoostUsedEvent : NetworkEvent
    {
        public BoostUsedEvent() { }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.BOOST_USED };
            return data;
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.BOOST_USED) throw new Exception("Invalid event type");

            return this;
        }
    }
}
