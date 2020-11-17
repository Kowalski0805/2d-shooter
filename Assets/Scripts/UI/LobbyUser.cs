using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUser : MonoBehaviour
{
    public Transform content;
    public GameObject Text;
    public GameObject button;

    void Start()
    {
        var text = Instantiate(Text, content);
        text.GetComponent<Text>().text = "Test";

        button.GetComponent<Button>().onClick.AddListener(StartGame);
    }

    public void AddPlayer((string NetworkID, string Username, string Ip) player)
    {
        var text = Instantiate(Text, content);
        text.GetComponent<Text>().text = player.Username + "[" + player.NetworkID + "] (" + player.Ip + ")";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateButton()
    {
        button.SetActive(true);
    }

    void StartGame() { 
        
    }

}
