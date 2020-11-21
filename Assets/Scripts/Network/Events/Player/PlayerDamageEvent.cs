using System;

namespace Assets.Scripts.Network.Events
{
    public class PlayerDamageEvent : NetworkEvent
    {
        int playerId = 0;
        int damage = 0;

        public PlayerDamageEvent() { }

        public PlayerDamageEvent(int playerId, int damage)
        {
            this.playerId = playerId;
            this.damage = damage;
        }

        public byte[] Serialize()
        {
            return new byte[] { (byte)Commands.PLAYER_DAMAGE, (byte) playerId, (byte) damage };
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.PLAYER_DAMAGE) throw new Exception("Invalid event type");

            playerId = data[1];
            damage = data[2];
            return this;
        }
    }
}
