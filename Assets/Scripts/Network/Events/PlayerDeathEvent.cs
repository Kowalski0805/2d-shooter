using System;
namespace Assets.Scripts.Network.Events
{
    public class PlayerDeathEvent : NetworkEvent
    {
        byte playerId = 0;

        public PlayerDeathEvent() { }

        public PlayerDeathEvent(byte playerId)
        {
            this.playerId = playerId;
        }

        public byte[] Serialize()
        {
            return new byte[] { (byte)Commands.PLAYER_DEATH, playerId };
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.PLAYER_DEATH) throw new Exception("Invalid event type");

            playerId = data[1];
            return this;
        }
    }
}
