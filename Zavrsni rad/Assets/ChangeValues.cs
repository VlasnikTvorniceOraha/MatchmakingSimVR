using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeValues : MonoBehaviour
{
    public GameObject EloTextInput;

    public GameObject PingTextInput;

    public PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change() 
    {
        //Debug.Log(EloTextInput.GetComponent<TMP_InputField>().text);

        if (EloTextInput.GetComponent<TMP_InputField>().text != "") {

            playerScript.Elo = int.Parse(EloTextInput.GetComponent<TMP_InputField>().text);

            playerScript.EloText.text = "Elo: " + playerScript.Elo;

        }

        if (PingTextInput.GetComponent<TMP_InputField>().text != "") {

            playerScript.basePing = int.Parse(PingTextInput.GetComponent<TMP_InputField>().text);

            playerScript.PingText.text = "Ping: " + playerScript.ping;

        }

    }
}
