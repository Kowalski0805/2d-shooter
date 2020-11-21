using System;
using System.Net;
using UnityEngine;

public class NetworkData
{

    public int NetworkID;
    public string Username;
    public bool connected = false;
    public bool isOwner;
    public IPEndPoint Ip;
    public NetworkPlayer player;

    public NetworkData() {}
}

public class NetworkPlayer : MonoBehaviour
{
    public bool isOwner = false;
    public float x = 0;
    public float y = 0;

    public void SetLocation(float x, float y)
    {
        transform.position = new Vector3(x, y, 0);
    }

    private void Update()
    {
        this.transform.Translate(new Vector3(x, y, 1) * Time.deltaTime);
    }
}
