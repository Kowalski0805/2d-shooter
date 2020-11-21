using Assets.Scripts.Network.Events;
using Assets.Scripts.Network.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomPlayerJoinedHandler : MonoBehaviour, ClientNetworkEventHandler
{
    private ExecuteOnMainThread _executeOnMainThread;

    public NetworkEvent Handle(Client client, NetworkEvent e)
    {
        RoomPlayerJoinedEvent jre = (RoomPlayerJoinedEvent)e;

        NetworkData player = new NetworkData();

        player.NetworkID = jre.userId;
        player.Username = jre.username;

        client.clientList[jre.userId] = player;

        _executeOnMainThread.RunOnMainThread.Enqueue(() => {
            FindObjectOfType<LobbyUser>().AddPlayer(client.clientList[jre.userId]);
        });

        if (client.id == 0 && client.clientList.FindLastIndex(c => c.Username != null) > 0)
        {
            _executeOnMainThread.RunOnMainThread.Enqueue(() => {
                FindObjectOfType<LobbyUser>().ActivateButton();
            });
        }

        return null;    
    }

    // Start is called before the first frame update
    void Start()
    {
        _executeOnMainThread = GameObject.FindWithTag("MainThread").GetComponent<ExecuteOnMainThread>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
