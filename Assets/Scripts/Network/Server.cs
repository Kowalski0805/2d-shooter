using Assets.Scripts.Network.Events;
using Assets.Scripts.Network.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Server : MonoBehaviour
{

    public GameObject playerPrefab;

    private UdpClient _server;
    private bool _serverRunning = true;
    public List<NetworkData> clientList = new List<NetworkData>(4);
    public Dictionary<Type, ServerNetworkEventHandler> handlers;
    public ExecuteOnMainThread _ExecuteOnMainThread;

    private int _count = 0;

    public void Start() {
        Debug.Log("Starting server");

        handlers = new Dictionary<Type, ServerNetworkEventHandler>() {
            { typeof(BoostUseEvent), FindObjectOfType<BoostUseHandler>() },
            { typeof(BulletSpawnEvent), FindObjectOfType<BulletSpawnHandler>() },
            { typeof(GameStartEvent), FindObjectOfType<GameStartHandler>() },
            { typeof(PlayerDisconnectEvent), FindObjectOfType<PlayerDisconnectHandler>() },
            { typeof(PlayerLoadEvent), FindObjectOfType<PlayerLoadHandler>() },
            { typeof(PlayerPositionEvent), FindObjectOfType<PlayerPositionHandler>() },
            { typeof(PongEvent), FindObjectOfType<PongHandler>() },
            { typeof(RoomJoinEvent), FindObjectOfType<RoomJoinHandler>() },
        };

        for (int i = 0; i < 4; i++)
        {
            NetworkData player = new NetworkData();
            var spawners = GameObject.FindGameObjectsWithTag("Spawner");
            var p = Instantiate(playerPrefab);
            p.transform.position = spawners[i].transform.position;
            var np = p.GetComponent<NetworkPlayer>();
            player.NetworkID = i;
            player.player = np;
            clientList.Add(player);
        }

        _ExecuteOnMainThread = GameObject.FindGameObjectWithTag("MainThread").GetComponent<ExecuteOnMainThread>();

        _server = new UdpClient(25575);
        new Thread(ServerThread).Start();
    }

    private void ServerThread()
    {
        Debug.Log("Started server");

        while (_serverRunning)
        {
            try
            {
                IPEndPoint clientIP = null;
                Debug.Log("Wait for packet");
                var data = _server.Receive(ref clientIP);
                Debug.Log("Received packet from " + clientIP.Address);

                //Debug.Log(Encoding.UTF8.GetString(data));
                //if (!Encoding.UTF8.GetString(data).Equals("init"))
                //    continue;
                Debug.Log(clientIP.Port);
                if (!clientList.Exists(c => Equals(c.Ip?.Address, clientIP.Address) && Equals(c.Ip?.Port, clientIP.Port)))
                {
                    if (_count >= 4)
                        Debug.LogError("More than 4");
                    Debug.Log("Create player");
                    Debug.Log(clientList.Select(er => er.NetworkID.ToString()).Aggregate((a, b) => a + ", " + b));
                    //NetworkPlayer new_player = Instantiate(playerPrefab).GetComponent<NetworkPlayer>();
                    //clientList.Add(new_player);
                    NetworkData new_player = clientList[_count];
                    new_player.Ip = clientIP;
                    new_player.NetworkID = _count;

                    _count++;
                }

                NetworkData player = clientList.Find(c => Equals(c.Ip?.Address, clientIP.Address) && c.Ip?.Port == clientIP.Port);
                // player.Ip = clientIP;
                NetworkEvent e = ReceiveEvent(data);
                Debug.Log("Get event " + e.GetType().ToString());

                ServerNetworkEventHandler handler = handlers[e.GetType()];
                handler.Handle(this, e, player);

                //new Thread(() => { ClientProcessor(clientIP); }).Start();

            }
            catch (Exception err) {
                Debug.Log(err.ToString());
            }
        }
    }

    private void ClientProcessor(IPEndPoint clientIP)
    {
        var resp = BitConverter.GetBytes(clientList.FindIndex(c => c.Ip == clientIP));

        var data = new byte[resp.Length + 1];

        data[0] = (byte) Commands.ID_RESPONSE;
        data.CopyTo(resp, 1);
        Send(data, clientIP);

        data[0] = (byte) Commands.PLAYER_CONNECT;
        
        SendToAll(data);
    }
    
    public void SendToAll(byte[] data)
    {
        foreach (var client in clientList)
        {
            _server.Send(data, data.Length, client.Ip);
        }
    }
    
    public void SendToAll(int data) {
        SendToAll(BitConverter.GetBytes(data));
    }
    
    public void SendToAll(string data) {
        SendToAll(Encoding.UTF8.GetBytes(data));
    }

    public void SendOthers(byte[] data, IPEndPoint except)
    {
        foreach (var client in clientList)
        {
            if (client.Ip != null && client.Ip != except)
            {
                Debug.Log("Send");
                _server.Send(data, data.Length, client.Ip);
            }
        }
    }

    public void SendOthers(int data, IPEndPoint except)
    {
        SendOthers(BitConverter.GetBytes(data), except);
    }

    public void SendOthers(string data, IPEndPoint except)
    {
        SendOthers(Encoding.UTF8.GetBytes(data), except);
    }

    public void Send(byte[] data, IPEndPoint client)
    {
        _server.Send(data, data.Length, client);
    }
    
    public void Send(string data, IPEndPoint client)
    {
        Send(Encoding.UTF8.GetBytes(data), client);
    }

    public void StopServer() {
        _serverRunning = false;
    }

    

    public NetworkEvent ReceiveEvent(byte[] data)
    {
        byte eventType = data[0];
        switch (eventType)
        {
            case (byte)Commands.BOOST_USE:
                return new BoostUseEvent().CreateEvent(data);
            case (byte)Commands.BULLET_SPAWN:
                return new BulletSpawnEvent().CreateEvent(data);
            case (byte)Commands.GAME_START:
                return new GameStartEvent().CreateEvent(data);
            case (byte)Commands.PLAYER_DISCONNECT:
                return new PlayerDisconnectEvent().CreateEvent(data);
            case (byte)Commands.PLAYER_LOAD:
                return new PlayerLoadEvent().CreateEvent(data);
            case (byte)Commands.PLAYER_POSITION:
                return new PlayerPositionEvent().CreateEvent(data);
            case (byte)Commands.PONG:
                return new PongEvent().CreateEvent(data);
            case (byte)Commands.ROOM_JOIN:
                return new RoomJoinEvent().CreateEvent(data);
            default:
                return null;
        }
    }

}
