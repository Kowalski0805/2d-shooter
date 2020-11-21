using System;
namespace Assets.Scripts.Network.Events
{
    public class PlayerDeathEvent : NetworkEvent
    {
        int playerId = 0;

        public PlayerDeathEvent() { }

        public PlayerDeathEvent(int playerId)
        {
            this.playerId = playerId;
        }

        public byte[] Serialize()
        {
            return new byte[] { (byte)Commands.PLAYER_DEATH, (byte) playerId };
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.PLAYER_DEATH) throw new Exception("Invalid event type");

            playerId = data[1];
            return this;
        }
    }
}
