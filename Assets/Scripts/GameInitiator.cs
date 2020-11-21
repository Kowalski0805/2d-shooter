using Assets.Scripts.Network.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    public GameObject playerPrefab;
    void Start()
    {
        var client = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Client>();
        client.SendEvent(new PlayerLoadEvent());
        client.clientList.ForEach(c =>
        {
            var p = Instantiate(playerPrefab);
            var spawners = GameObject.FindGameObjectsWithTag("Spawner");
            p.transform.position = spawners[c.NetworkID].transform.position;
            if (c.NetworkID == client.id)
            {
                c.player = p.GetComponent<NetworkPlayer>();
                c.player.isOwner = true;
                p.GetComponent<PlayerControl>().enabled = true;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
