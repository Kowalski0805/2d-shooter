using Assets.Scripts.Network.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private float _currentSpeed;
    private Rigidbody2D _playerRigidbody2D;

    public int speed = 5;
    public int rotationSpeed = 5;
    public GameObject bullet;


    void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        int acceleration = (int) Input.GetAxis("Vertical");
        int rotation = (int) -Input.GetAxis("Horizontal");

        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Client>().SendEvent(new PlayerPositionEvent(acceleration, rotation));
    }

    private void Move(int acceleration, int rotation)
    {
        Vector2 speedForce = transform.up * (acceleration * speed);

        transform.Rotate(new Vector3(0, 0, rotation * rotationSpeed));

        _playerRigidbody2D.velocity = speedForce;

        if (acceleration == 0)
            _playerRigidbody2D.velocity = Vector2.zero;

        if (Input.GetButtonDown("Fire1"))
            Fire();
    }

    private void Fire()
    {
        var newBullet = Instantiate(bullet);
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = transform.rotation;
        newBullet.GetComponent<Rigidbody2D>().AddForce(newBullet.transform.up * 50, ForceMode2D.Impulse);
    }
}
