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
    class GameStartedHandler : MonoBehaviour, ClientNetworkEventHandler {

        public NetworkEvent Handle(Client client, NetworkEvent e) {
            client._ExecuteOnMainThread.RunOnMainThread.Enqueue(() => {
                SceneManager.LoadScene("GameScene");
            });
            return null;
        }
    }
}
