using System;

namespace Assets.Scripts.Network.Events
{
    public class PlayerPositionEvent : NetworkEvent
    {
        byte id = 0;
        byte x = 0;
        byte y = 0;

        public PlayerPositionEvent() { }

        public PlayerPositionEvent(byte id, byte x, byte y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
        }

        public byte[] Serialize()
        {
            return new byte[] { (byte) Commands.POSITION_INFO, id, x, y };
        }

        public NetworkEvent CreateEvent(byte[] data) {
            if (data[0] != (byte)Commands.POSITION_INFO) throw new Exception("Invalid event type");

            id = data[1];
            x = data[2];
            y = data[3];
            return this;
        }
    }
}
