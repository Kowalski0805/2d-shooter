using Assets.Scripts.Network.Events;
using Assets.Scripts.Network.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomJoinedHandler : MonoBehaviour, ClientNetworkEventHandler {
    private ExecuteOnMainThread _executeOnMainThread;
    
    public NetworkEvent Handle(Client client, NetworkEvent e)
    {
        RoomJoinedEvent jre = (RoomJoinedEvent)e;
        client.id = jre.userId;

        client.clientList = jre.players;
        
        foreach (var player in client.clientList) {
            if (player.Username != null)
            {
                _executeOnMainThread.RunOnMainThread.Enqueue(() => {
                    FindObjectOfType<LobbyUser>().AddPlayer(player);
                });
            }
        }

        return null;
    }

    // Start is called before the first frame update
    void Start() {
        _executeOnMainThread = GameObject.FindWithTag("MainThread").GetComponent<ExecuteOnMainThread>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
