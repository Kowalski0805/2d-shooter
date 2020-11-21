using Assets.Scripts.Network.Events;
using Assets.Scripts.Network.Handlers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Client : MonoBehaviour
{

    private UdpClient _udp;
    public int id;

    public List<NetworkData> clientList = new List<NetworkData>();
    public Dictionary<Type, ClientNetworkEventHandler> handlers;
    public ExecuteOnMainThread _ExecuteOnMainThread;

    public void Start()
    {
        Debug.Log("Starting client");

        handlers = new Dictionary<Type, ClientNetworkEventHandler>() {
            { typeof(BoostEndEvent), FindObjectOfType<BoostEndHandler>() },
            { typeof(BoostSpawnEvent), FindObjectOfType<BoostSpawnHandler>() },
            { typeof(BoostUsedEvent), FindObjectOfType<BoostUsedHandler>() },
            { typeof(BulletSpawnedEvent), FindObjectOfType<BulletSpawnedHandler>() },
            { typeof(GameStartedEvent), FindObjectOfType<GameStartedHandler>() },
            { typeof(PingEvent), FindObjectOfType<PingHandler>() },
            { typeof(PlayerDamageEvent), FindObjectOfType<PlayerDamageHandler>() },
            { typeof(PlayerDeathEvent), FindObjectOfType<PlayerDeathHandler>() },
            { typeof(PlayerDisconnectedEvent), FindObjectOfType<PlayerDisconnectedHandler>() },
            { typeof(PlayerLoadedEvent), FindObjectOfType<PlayerLoadedHandler>() },
            { typeof(PlayerPositionedEvent), FindObjectOfType<PlayerPositionedHandler>() },
            { typeof(RoomJoinedEvent), FindObjectOfType<RoomJoinedHandler>() },
            { typeof(RoomPlayerJoinedEvent), FindObjectOfType<RoomPlayerJoinedHandler>() },
        };

        _udp = new UdpClient();
        _ExecuteOnMainThread = GameObject.FindGameObjectWithTag("MainThread").GetComponent<ExecuteOnMainThread>();

        DontDestroyOnLoad(this);
    }

    public void Connect(string address, string username)
    {
        Debug.Log("Connecting");

        _udp.Connect(address, 25575);
        Send(new RoomJoinEvent(username).Serialize());
        while (true)
        {
            Debug.Log("Wait for response");

            IPEndPoint serverAddress = null;
            var data = _udp.Receive(ref serverAddress);
            ReceiveCallback(data);
        }
    }

    private void ReceiveCallback(byte[] data)
    {
        //Debug.Log(Encoding.UTF8.GetString(data));
        NetworkEvent e = ReceiveEvent(data);
        Debug.Log("Parse event " + e.GetType().ToString());

        if (e != null)
        {
            handlers[e.GetType()].Handle(this, e);
        }
    }

    private void Send(byte[] data)
    {
        _udp.Send(data, data.Length);
    }

    private void Send(string data)
    {
        Send(Encoding.UTF8.GetBytes(data));
    }

    public void SendEvent(NetworkEvent e)
    {
        Send(e.Serialize());
    }

    public NetworkEvent ReceiveEvent(byte[] data)
    {
        byte eventType = data[0];
        switch (eventType)
        {
            case (byte)Commands.BOOST_END:
                return new BoostEndEvent().CreateEvent(data);
            case (byte)Commands.BOOST_SPAWN:
                return new BoostSpawnEvent().CreateEvent(data);
            case (byte)Commands.BOOST_USED:
                return new BoostUsedEvent().CreateEvent(data);
            case (byte)Commands.BULLET_SPAWNED:
                return new BulletSpawnedEvent().CreateEvent(data);
            case (byte)Commands.GAME_STARTED:
                return new GameStartedEvent().CreateEvent(data);
            case (byte)Commands.PING:
                return new PingEvent().CreateEvent(data);
            case (byte)Commands.PLAYER_DAMAGE:
                return new PlayerDamageEvent().CreateEvent(data);
            case (byte)Commands.PLAYER_DEATH:
                return new PlayerDeathEvent().CreateEvent(data);
            case (byte)Commands.PLAYER_DISCONNECTED:
                return new PlayerDisconnectedEvent().CreateEvent(data);
            case (byte)Commands.PLAYER_LOADED:
                return new PlayerLoadedEvent().CreateEvent(data);
            case (byte)Commands.PLAYER_POSITIONED:
                return new PlayerPositionedEvent().CreateEvent(data);
            case (byte)Commands.ROOM_JOINED:
                return new RoomJoinedEvent().CreateEvent(data);
            case (byte)Commands.ROOM_PLAYER_JOINED:
                return new RoomPlayerJoinedEvent().CreateEvent(data);
            default:
                return null;
        }
    }
}
