using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Network.Events
{
    public class RoomJoinedEvent : NetworkEvent
    {
        public int userId;
        public List<NetworkData> players = new List<NetworkData>();

        public RoomJoinedEvent() { }

        public RoomJoinedEvent(int userId, List<NetworkData> players)
        {
            this.userId = userId;
            this.players = players;
        }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.ROOM_JOINED, (byte)userId };

            string playerz = Encode(players);
            Debug.Log(playerz);
            return data.Concat(Encoding.UTF8.GetBytes(playerz)).ToArray();
        }

        private string Encode(List<NetworkData> list)
        {
            return string.Join(";", list.Select(e => string.Join(",", new string[] { e.NetworkID.ToString(), e.Username })));
        }

        private List<NetworkData> Decode(string data)
        {
            List<NetworkData> rows = data.Split(new char[] { ';' }).Select(row => {
                string[] record = row.Split(new char[] { ',' });
                NetworkData p = new NetworkData();
                p.NetworkID = int.Parse(record[0]);
                p.Username = record.Length == 2 ? record[1] : null;

                return p;
            }).ToList();

            return rows;
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.ROOM_JOINED) throw new Exception("Invalid event type");

            userId = data[1];
            players = Decode(Encoding.UTF8.GetString(data.Skip(2).ToArray()));
            return this;
        }
    }
}
