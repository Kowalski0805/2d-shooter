using Assets.Scripts.Network.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private float _currentSpeed;

    public GameObject bullet;


    void Start()
    {
    }

    private void Update()
    {
        int acceleration = (int) Input.GetAxis("Vertical");
        int rotation = (int) -Input.GetAxis("Horizontal");

        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Client>().SendEvent(new PlayerPositionEvent(acceleration, rotation));
    }

    private void Move(int acceleration, int rotation)
    {
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
