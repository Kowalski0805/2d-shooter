using Assets.Scripts.Network.Events;
using Assets.Scripts.Network.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameStartHandler : MonoBehaviour, ServerNetworkEventHandler
{
    public NetworkEvent Handle(Server server, NetworkEvent e, NetworkData player)
    {
        server.SendToAll(new GameStartedEvent().Serialize());
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
