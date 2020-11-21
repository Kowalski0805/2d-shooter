﻿using Assets.Scripts.Network.Events;
using Assets.Scripts.Network.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerLoadHandler : MonoBehaviour, ServerNetworkEventHandler
{
    public NetworkEvent Handle(Server server, NetworkEvent e, NetworkData player)
    {
        player.connected = true;

        if (server.clientList.TrueForAll(p => p.connected))
        {
            server.SendToAll(new PlayerLoadedEvent().Serialize());
        }

        return null;
    }
}