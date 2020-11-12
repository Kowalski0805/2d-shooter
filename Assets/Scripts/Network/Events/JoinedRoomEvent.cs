using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace Assets.Scripts.Network.Events
{
    public class JoinedRoomEvent : NetworkEvent
    {
        public int userId;
        public List<(int, string)> players = new List<(int, string)>();

        public JoinedRoomEvent() { }

        public JoinedRoomEvent(int userId, List<Network.NetworkPlayer> players)
        {
            this.userId = userId;
            this.players = players.Select(p => (id: p.NetworkID, username: p.Username)).ToList();
        }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.JOINED_ROOM, (byte)userId };
            return data.Concat(Encoding.UTF8.GetBytes(JsonUtility.ToJson(players))).ToArray();
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.JOINED_ROOM) throw new Exception("Invalid event type");

            userId = data[1];
            return this;
        }
    }
}
