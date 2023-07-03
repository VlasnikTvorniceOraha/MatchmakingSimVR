using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public int Elo;
    public int basePing;

    public int ping;

    public int vrijeme;

    public TMP_Text EloText;

    public TMP_Text PingText;

    public TMP_Text VrijemeText;

    public bool InQueue;

    public int timeSpentEligible = 0;

    public TMP_Text ime;

    // Start is called before the first frame update
    void Start()
    {
        Elo = Random.Range(100, 1700);
        basePing = Random.Range(10, 210);
        ping = basePing;

        EloText.text = EloText.text + " " + Elo;

        PingText.text = PingText.text + " " + ping;

        ime.text = this.name;


        //generate random ping spikes
        InvokeRepeating("VariablePing", 0.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void VariablePing() {

        int randomPingoffset = Random.Range(-10, 10);

        ping = basePing + randomPingoffset;

        PingText.text = "Ping: " + ping;

    }
}
