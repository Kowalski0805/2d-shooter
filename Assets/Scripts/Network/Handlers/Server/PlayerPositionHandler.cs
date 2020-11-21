using Assets.Scripts.Network.Events;
using Assets.Scripts.Network.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPositionHandler : MonoBehaviour, ServerNetworkEventHandler
{
    public NetworkEvent Handle(Server server, NetworkEvent e, NetworkData player)
    {
        // TODO: implement
        PlayerPositionEvent ppe = (PlayerPositionEvent)e;

        server._ExecuteOnMainThread.RunOnMainThread.Enqueue(() => {
            GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>().Control(player.NetworkID, ppe.x, ppe.y);
        });

        return null;
    }
}
