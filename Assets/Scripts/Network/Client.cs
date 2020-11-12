using Assets.Scripts.Network.Events;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

    public class Client : MonoBehaviour {

        private readonly UdpClient _udp;

        public Client() {
            _udp = new UdpClient();
        }

        public void Connect(string address, string username) {
            _udp.Connect(address, 25575);
            Send(new JoinRoomEvent(username).Serialize());
            while (true) {
                IPEndPoint serverAddress = null;
                var data = _udp.Receive(ref serverAddress);
                ReceiveCallback(Encoding.UTF8.GetString(data));
            }
        }

        private void ReceiveCallback(string data) {
            Debug.Log(data);
        }

        private void Send(byte[] data) {
            _udp.Send(data, data.Length);
        }

        private void Send(string data) {
            Send(Encoding.UTF8.GetBytes(data));
        }

        public void SendEvent(NetworkEvent e) {
            Send(e.Serialize());
        }

        public NetworkEvent ReceiveEvent(byte[] data) {
            byte eventType = data[0];
            switch (eventType) {
                case (byte) Commands.POSITION_INFO:
                    return new PlayerPositionEvent().CreateEvent(data);
                case (byte) Commands.PLAYER_DAMAGE:
                    return new PlayerDamageEvent().CreateEvent(data);
                default:
                return null;
            }
        }
    }
