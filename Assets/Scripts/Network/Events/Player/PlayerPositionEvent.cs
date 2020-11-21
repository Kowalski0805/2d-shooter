using System;

namespace Assets.Scripts.Network.Events
{
    public class PlayerPositionEvent : NetworkEvent
    {
        int x = 0;
        int y = 0;

        public PlayerPositionEvent() { }

        public PlayerPositionEvent(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public byte[] Serialize()
        {
            return new byte[] { (byte) Commands.PLAYER_POSITION, (byte) x, (byte) y };
        }

        public NetworkEvent CreateEvent(byte[] data) {
            if (data[0] != (byte)Commands.PLAYER_POSITION) throw new Exception("Invalid event type");

            x = data[1];
            y = data[2];
            return this;
        }
    }
}
