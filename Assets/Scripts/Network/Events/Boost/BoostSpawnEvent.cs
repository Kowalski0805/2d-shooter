using System;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    // TODO: implement
    public class BoostSpawnEvent : NetworkEvent
    {
        public BoostSpawnEvent() { }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.BOOST_SPAWN };
            return data;
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.BOOST_SPAWN) throw new Exception("Invalid event type");

            return this;
        }
    }
}
