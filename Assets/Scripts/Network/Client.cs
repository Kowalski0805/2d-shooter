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

    public List<(string NetworkID, string Username, string Ip)> clientList = new List<(string, string, string)>();
    public Dictionary<Type, ClientNetworkEventHandler> handlers;

    public void Start()
    {
        Debug.Log("Starting client");

        handlers = new Dictionary<Type, ClientNetworkEventHandler>() {
            { typeof(JoinedRoomEvent), FindObjectOfType<JoinedRoomHandler>() }
        };

        _udp = new UdpClient();

        //DontDestroyOnLoad(this);
    }

    public void Connect(string address, string username)
    {
        Debug.Log("Connecting");

        _udp.Connect(address, 25575);
        Send(new JoinRoomEvent(username).Serialize());
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
            case (byte)Commands.JOINED_ROOM:
                return new JoinedRoomEvent().CreateEvent(data);
            case (byte)Commands.POSITION_INFO:
                return new PlayerPositionEvent().CreateEvent(data);
            case (byte)Commands.PLAYER_DAMAGE:
                return new PlayerDamageEvent().CreateEvent(data);
            default:
                return null;
        }
    }
}
