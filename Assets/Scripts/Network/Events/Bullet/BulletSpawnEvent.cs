using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    public class BulletSpawnEvent : NetworkEvent
    {

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.BULLET_SPAWN };

            return data;
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.BULLET_SPAWN) throw new Exception("Invalid event type");

            return this;
        }
    }
}
