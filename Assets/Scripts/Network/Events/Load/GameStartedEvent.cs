using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    public class GameStartedEvent : NetworkEvent
    {
        public List<(float, float)> coords;

        public GameStartedEvent() { }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.GAME_STARTED };

            return data;
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.GAME_STARTED) throw new Exception("Invalid event type");



            return this;
        }
    }
}
