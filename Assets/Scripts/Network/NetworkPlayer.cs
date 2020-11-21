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

    public void SetLocation(float x, float y)
    {
        transform.position = new Vector3(x, y, 0);
    }
}
