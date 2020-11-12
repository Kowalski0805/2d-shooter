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
        Instantiate(Text, content);
        Instantiate(Text, content);

        text.GetComponent<Text>().text = "Test";

        button.GetComponent<Button>().onClick.AddListener(StartGame);
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
