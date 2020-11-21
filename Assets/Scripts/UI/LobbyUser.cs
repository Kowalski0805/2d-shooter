using Assets.Scripts.Network.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUser : MonoBehaviour
{
    public Transform content;
    public GameObject button;
    public GameObject[] playerTexts;

    void Start()
    {
        button.GetComponent<Button>().onClick.AddListener(StartGame);
    }

    public void AddPlayer(NetworkData player)
    {
        var text = playerTexts[player.NetworkID];
        var component = text.GetComponent<Text>();
        component.text = player.Username + "[" + player.NetworkID + "]";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateButton()
    {
        button.SetActive(true);
    }

    void StartGame() {
        FindObjectOfType<Client>().SendEvent(new GameStartEvent());
    }

}
