using Assets.Scripts.Network.Events;
using Assets.Scripts.Network.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLoadedHandler : MonoBehaviour, ClientNetworkEventHandler
{
    private ExecuteOnMainThread _executeOnMainThread;

    public NetworkEvent Handle(Client client, NetworkEvent e)
    {
        client.clientList.Find(c => c.NetworkID == client.id);

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
