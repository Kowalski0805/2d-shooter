using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    public class PlayerLoadEvent : NetworkEvent
    {

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.PLAYER_LOAD };

            return data;
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.PLAYER_LOAD) throw new Exception("Invalid event type");



            return this;
        }
    }
}
