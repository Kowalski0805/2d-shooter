using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour {

    public InputField username;
    public InputField address;
    public Button connectButton;
    public Client client;
    
    private void Start() {
        connectButton.onClick.AddListener(Connect);
    }

    private void Connect() {
        DontDestroyOnLoad(client);
        new Thread(() => {
            try {
                client.Connect(address.text, username.text);
            }
            catch (Exception err) {
                Debug.Log(err.ToString());
            }
        }).Start();
    }
}