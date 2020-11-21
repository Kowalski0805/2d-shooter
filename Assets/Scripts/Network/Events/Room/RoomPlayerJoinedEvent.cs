using System;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    public class RoomPlayerJoinedEvent : NetworkEvent
    {
        public int userId;
        public string username;

        public RoomPlayerJoinedEvent() { }

        public RoomPlayerJoinedEvent(int userId, string username)
        {
            this.userId = userId;
            this.username = username;

        }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.ROOM_PLAYER_JOINED, (byte)userId };
            return data.Concat(Encoding.UTF8.GetBytes(username)).ToArray();
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.ROOM_PLAYER_JOINED) throw new Exception("Invalid event type");

            userId = data[1];
            username = Encoding.UTF8.GetString(data.Skip(2).ToArray());
            return this;
        }
    }
}
