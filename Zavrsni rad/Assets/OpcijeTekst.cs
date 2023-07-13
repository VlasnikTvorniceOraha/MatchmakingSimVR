using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpcijeTekst : MonoBehaviour
{

    public GameObject queue;
    public GameObject self;

    // Start is called before the first frame update
    void Start()
    {
        
        StartQueue qSkripta = queue.GetComponent<StartQueue>();

        Transform granicaElo = self.transform.Find("Pocetna Granica za Elo");
        granicaElo.Find("box").GetComponent<TMP_Text>().text = qSkripta.baseEloDiff.ToString();

        Transform skalaElo = self.transform.Find("SkalaElo");
        skalaElo.Find("box").GetComponent<TMP_Text>().text = qSkripta.EloTimeScale.ToString();

        Transform granicaPing = self.transform.Find("Pocetna Granica za Ping");
        granicaPing.Find("box").GetComponent<TMP_Text>().text = qSkripta.basePingDiff.ToString();

        Transform skalaPing = self.transform.Find("SkalaPing");
        skalaPing.Find("box").GetComponent<TMP_Text>().text = qSkripta.PingTimeScale.ToString();

        Transform timeReq = self.transform.Find("TimeReq");
        timeReq.Find("box").GetComponent<TMP_Text>().text = qSkripta.timeRequirementForMatch.ToString();



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
