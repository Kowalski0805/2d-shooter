using System;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    // TODO: implement
    public class BoostEndEvent : NetworkEvent
    {
        public BoostEndEvent() { }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.BOOST_END };
            return data;
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.BOOST_END) throw new Exception("Invalid event type");

            return this;
        }
    }
}
