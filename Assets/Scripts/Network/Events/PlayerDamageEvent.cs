using System;

namespace Assets.Scripts.Network.Events
{
    public class PlayerDamageEvent : NetworkEvent
    {
        byte playerId = 0;
        byte bulletId = 0;

        public PlayerDamageEvent() { }

        public PlayerDamageEvent(byte playerId, byte bulletId)
        {
            this.playerId = playerId;
            this.bulletId = bulletId;
        }

        public byte[] Serialize()
        {
            return new byte[] { (byte)Commands.PLAYER_DAMAGE, playerId, bulletId };
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.PLAYER_DAMAGE) throw new Exception("Invalid event type");

            playerId = data[1];
            bulletId = data[2];
            return this;
        }
    }
}
