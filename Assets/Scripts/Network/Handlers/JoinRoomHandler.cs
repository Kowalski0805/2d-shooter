using Assets.Scripts.Network.Events;
using Assets.Scripts.Network.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class JoinRoomHandler : MonoBehaviour, ServerNetworkEventHandler
{
    public NetworkEvent Handle(Server server, NetworkEvent e, NetworkPlayer player)
    {
        JoinRoomEvent jre = (JoinRoomEvent)e;
        player.Username = jre.username;

        Debug.Log(jre);

        server.Send(new JoinedRoomEvent(player.NetworkID, server.clientList).Serialize(), player.Ip);

        server.SendOthers(new PlayerJoinedRoomEvent(player.NetworkID, player.Username).Serialize(), player.Ip);
        
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
