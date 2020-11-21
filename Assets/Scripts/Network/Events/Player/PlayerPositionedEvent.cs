using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Network.Events
{
    public class PlayerPositionedEvent : NetworkEvent
    {
        public List<(float x, float y, float rot)> coords;

        public PlayerPositionedEvent() { }

        public PlayerPositionedEvent(List<NetworkPlayer> players)
        {
            coords = players.Select(p => (p.transform.position.x, p.transform.position.y, rot: p.transform.rotation.eulerAngles.z)).ToList();
        }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte) Commands.PLAYER_POSITIONED };

            return data.Concat(coords.SelectMany(c => BitConverter.GetBytes(c.x).Concat(BitConverter.GetBytes(c.y)).Concat(BitConverter.GetBytes(c.rot)))).ToArray();
        }

        public NetworkEvent CreateEvent(byte[] data) {
            if (data[0] != (byte)Commands.PLAYER_POSITIONED) throw new Exception("Invalid event type");
            if (data.Length - 1 % 12 != 0) throw new Exception("Incomplete packet!");

            for (int i = 1; i < data.Length; i += 12)
            {
                float x = BitConverter.ToSingle(data, i);
                float y = BitConverter.ToSingle(data, i + 4);
                float rot = BitConverter.ToSingle(data, i + 8);

                coords.Add((x, y, rot));
            }

            return this;
        }
    }
}
