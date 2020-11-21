using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Network.Events;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    private GameObject[] _players;
    public Server _server;
    public int speed = 5;
    public int rotationSpeed = 5;

    public bool isEnabled = false;

    void Start()
    {
        Debug.Log("Start");
        _players = GameObject.FindGameObjectsWithTag("Player");
        isEnabled = true;
    }

    void Update()
    {
        if (isEnabled)
            _server.SendToAll(new PlayerPositionedEvent(_players.Select(p => p.GetComponent<NetworkPlayer>()).ToList()).Serialize());
    }

    public void Control(int playerId, int acceleration, int rotation)
    {
        Debug.Log(_players.Length.ToString() + " " + playerId.ToString());
        Rigidbody2D _playerRigidbody2D = _players[playerId].GetComponent<Rigidbody2D>();

        Vector2 speedForce = transform.up * (acceleration * speed);

        transform.Rotate(new Vector3(0, 0, rotation * rotationSpeed));

        _playerRigidbody2D.velocity = speedForce;

        if (acceleration == 0)
            _playerRigidbody2D.velocity = Vector2.zero;
    }
}
