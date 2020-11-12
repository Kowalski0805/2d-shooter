using System;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Events
{
    public class JoinRoomEvent : NetworkEvent
    {
        public string username;

        public JoinRoomEvent() { }

        public JoinRoomEvent(string username)
        {
            this.username = username;
        }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.JOIN_ROOM };
            return data.Concat(Encoding.UTF8.GetBytes(username)).ToArray();
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.JOIN_ROOM) throw new Exception("Invalid event type");

            username = Encoding.UTF8.GetString(data.Skip(1).ToArray()); ;
            return this;
        }
    }
}
