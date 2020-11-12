using System;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    public class PlayerJoinedRoomEvent : NetworkEvent
    {
        public int userId;
        public string username;

        public PlayerJoinedRoomEvent() { }

        public PlayerJoinedRoomEvent(int userId, string username)
        {
            this.userId = userId;
            this.username = username;

        }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.JOINED_ROOM, (byte)userId };
            return data.Concat(Encoding.UTF8.GetBytes(username)).ToArray();
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.JOINED_ROOM) throw new Exception("Invalid event type");

            userId = data[1];
            username = Encoding.UTF8.GetString(data.Skip(2).ToArray());
            return this;
        }
    }
}
