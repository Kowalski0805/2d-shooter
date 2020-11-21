using Assets.Scripts.Network.Events;
using Assets.Scripts.Network.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomJoinHandler : MonoBehaviour, ServerNetworkEventHandler
{
    public NetworkEvent Handle(Server server, NetworkEvent e, NetworkData player)
    {
        RoomJoinEvent jre = (RoomJoinEvent)e;
        player.Username = jre.username;

        Debug.Log(jre);

        server.Send(new RoomJoinedEvent(player.NetworkID, server.clientList).Serialize(), player.Ip);

        server.SendOthers(new RoomPlayerJoinedEvent(player.NetworkID, player.Username).Serialize(), player.Ip);
        
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
