using Assets.Scripts.Network.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Network.Handlers
{
    class PlayerPositionedHandler : MonoBehaviour, ClientNetworkEventHandler
    {
        public NetworkEvent Handle(Client client, NetworkEvent e)
        {
            // TODO: implement
            PlayerPositionedEvent ppe = (PlayerPositionedEvent)e;

            for (int i = 0; i < ppe.coords.Count; i++)
            {
                var player = ppe.coords[i];

                client._ExecuteOnMainThread.RunOnMainThread.Enqueue(() =>
                {
                    PlayerNetwork pn = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerNetwork>();

                    pn.Control(i, player.x, player.y, player.rot);
                });
            }

            return null;
        }
    }
}
