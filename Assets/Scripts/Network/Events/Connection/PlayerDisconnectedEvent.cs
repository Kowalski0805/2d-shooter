using System;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    // TODO: implement
    public class PlayerDisconnectedEvent : NetworkEvent
    {
        public PlayerDisconnectedEvent() { }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.PLAYER_DISCONNECTED };
            return data;
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.PLAYER_DISCONNECTED) throw new Exception("Invalid event type");

            return this;
        }
    }
}
