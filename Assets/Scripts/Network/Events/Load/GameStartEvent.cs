using System;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    public class GameStartEvent : NetworkEvent
    {
        public GameStartEvent() { }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.GAME_START };
            return data;
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.GAME_START) throw new Exception("Invalid event type");

            return this;
        }
    }
}
