using System;
using System.Net;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{

    public int NetworkID;
    public string Username;
    public IPEndPoint Ip;

    public NetworkPlayer(IPEndPoint ip)
    {
        Ip = ip;
    }

    public NetworkPlayer(IPEndPoint ip, string username, int networkID)
    {
        Ip = ip;
        Username = username;
        NetworkID = networkID;
    }

    public void SetLocation(float x, float y)
    {
        transform.position = new Vector3(x, y, 0);
    }

    public void SetId(int id)
    {
        NetworkID = id;
    }
}
