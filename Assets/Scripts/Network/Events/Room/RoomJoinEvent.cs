using System;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    public class RoomJoinEvent : NetworkEvent
    {
        public string username;

        public RoomJoinEvent() { }

        public RoomJoinEvent(string username)
        {
            this.username = username;
        }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.ROOM_JOIN };
            return data.Concat(Encoding.UTF8.GetBytes(username)).ToArray();
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.ROOM_JOIN) throw new Exception("Invalid event type");

            username = Encoding.UTF8.GetString(data.Skip(1).ToArray()); ;
            return this;
        }
    }
}
