using Assets.Scripts.Network.Events;
using Assets.Scripts.Network.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinedRoomHandler : MonoBehaviour, ClientNetworkEventHandler {
    private ExecuteOnMainThread _executeOnMainThread;
    
    public NetworkEvent Handle(Client client, NetworkEvent e)
    {
        JoinedRoomEvent jre = (JoinedRoomEvent)e;
        client.id = jre.userId;

        client.clientList = jre.players;

        (string NetworkID, string Username, string Ip) player = client.clientList[0];


        Debug.Log(player.Username + "[" + player.NetworkID + "] (" + player.Ip+ ")");
        foreach (var playero in client.clientList) {
            _executeOnMainThread.RunOnMainThread.Enqueue(() => {
                FindObjectOfType<LobbyUser>().AddPlayer(playero);
            });
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
