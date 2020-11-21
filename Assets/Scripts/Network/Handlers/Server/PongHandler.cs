using Assets.Scripts.Network.Events;
using Assets.Scripts.Network.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PongHandler : MonoBehaviour, ServerNetworkEventHandler
{
    public NetworkEvent Handle(Server server, NetworkEvent e, NetworkData player)
    {
        // TODO: implement
        return null;
    }
}
