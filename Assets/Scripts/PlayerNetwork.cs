using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Network.Events;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
    private GameObject[] _players;
    private Client _client;

    void Start()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
        _client = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Client>();
    }

    void Update()
    {
        _client.SendEvent(new PlayerPositionedEvent(_players.Select(p => p.GetComponent<NetworkPlayer>()).ToList()));
    }

    public void Control(int playerId, float x, float y, float rot)
    {
        NetworkPlayer player = _players[playerId].GetComponent<NetworkPlayer>();
        player.x = x;
        player.y = y;
    }
}

