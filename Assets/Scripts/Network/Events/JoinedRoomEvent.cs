using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Network.Events
{
    public class JoinedRoomEvent : NetworkEvent
    {
        public int userId;
        public List<(string NetworkID, string Username, string Ip)> players = new List<(string, string, string)>();

        public JoinedRoomEvent() { }

        public JoinedRoomEvent(int userId, List<NetworkPlayer> players)
        {
            this.userId = userId;
            this.players = players.Select(e => (NetworkID: e.NetworkID.ToString(), Username: e.Username, Ip: e.Ip?.Address?.ToString())).ToList();
        }

        public byte[] Serialize()
        {
            byte[] data = new byte[] { (byte)Commands.JOINED_ROOM, (byte)userId };

            string playerz = Encode(players);
            Debug.Log(playerz);
            return data.Concat(Encoding.UTF8.GetBytes(playerz)).ToArray();
        }

        private string Encode(List<(string NetworkID, string Username, string Ip)> list)
        {
            return string.Join(";", list.Select(e => string.Join(",", new string[] { e.NetworkID, e.Username, e.Ip })));
        }

        private List<(string NetworkID, string Username, string Ip)> Decode(string data)
        {
            List<(string, string, string)> rows = data.Split(new char[] { ';' }).Select(row => {
                string[] record = row.Split(new char[] { ',' });
                return (NetworkID: record[0], Username: record[1], Ip: record[2]);
            }).ToList();

            return rows;
        }

        public NetworkEvent CreateEvent(byte[] data)
        {
            if (data[0] != (byte)Commands.JOINED_ROOM) throw new Exception("Invalid event type");

            userId = data[1];
            players = Decode(Encoding.UTF8.GetString(data.Skip(2).ToArray()));
            return this;
        }
    }
}
