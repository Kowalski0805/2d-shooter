using System;
using System.Linq;

namespace Assets.Scripts.Network.Events
{
    public class BulletSpawnedEvent : NetworkEvent
    {
        float x = 0;
        float y = 0;
        float rot = 0;

        public BulletSpawnedEvent() { }

        public BulletSpawnedEvent(float x, float y, float rot)
        {
            this.x = x;
            this.y = y;
            this.rot = rot;
        }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.BULLET_SPAWNED };

            return data.Concat(BitConverter.GetBytes(x)).Concat(BitConverter.GetBytes(y)).Concat(BitConverter.GetBytes(rot)).ToArray();
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.PLAYER_POSITION) throw new Exception("Invalid event type");

            x = BitConverter.ToSingle(data, 1);
            y = BitConverter.ToSingle(data, 5);
            rot = BitConverter.ToSingle(data, 9);
            return this;
        }
    }
}
