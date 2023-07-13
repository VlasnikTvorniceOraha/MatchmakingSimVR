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

    public GameObject EloGranica;

    public GameObject SkalaElo;

    public GameObject PingGranica;

    public GameObject SkalaPing;

    public GameObject TimeReq;

    public void Change() 
    {
        //Debug.Log(EloTextInput.GetComponent<TMP_InputField>().text);

        StartQueue qSkripta = queue.GetComponent<StartQueue>();



        if (EloGranica.GetComponent<TMP_InputField>().text != "") {

            qSkripta.baseEloDiff = int.Parse(EloGranica.GetComponent<TMP_InputField>().text);
            Transform granicaElo = self.transform.Find("Pocetna Granica za Elo");
            granicaElo.Find("box").GetComponent<TMP_Text>().text = EloGranica.GetComponent<TMP_InputField>().text;

        }

        if (SkalaElo.GetComponent<TMP_InputField>().text != "") {

            qSkripta.EloTimeScale = int.Parse(SkalaElo.GetComponent<TMP_InputField>().text);
            Transform skalaElo = self.transform.Find("SkalaElo");
            skalaElo.Find("box").GetComponent<TMP_Text>().text = SkalaElo.GetComponent<TMP_InputField>().text;

        }

        if (PingGranica.GetComponent<TMP_InputField>().text != "") {

            qSkripta.basePingDiff = int.Parse(PingGranica.GetComponent<TMP_InputField>().text);
            Transform granicaPing = self.transform.Find("Pocetna Granica za Ping");
            granicaPing.Find("box").GetComponent<TMP_Text>().text = PingGranica.GetComponent<TMP_InputField>().text;

        }

        if (SkalaPing.GetComponent<TMP_InputField>().text != "") {

            qSkripta.PingTimeScale = int.Parse(SkalaPing.GetComponent<TMP_InputField>().text);
            Transform skalaPing = self.transform.Find("SkalaPing");
            skalaPing.Find("box").GetComponent<TMP_Text>().text = SkalaPing.GetComponent<TMP_InputField>().text;

        }

        if (TimeReq.GetComponent<TMP_InputField>().text != "") {

            qSkripta.timeRequirementForMatch = int.Parse(TimeReq.GetComponent<TMP_InputField>().text);
            Transform timeReq = self.transform.Find("TimeReq");
            timeReq.Find("box").GetComponent<TMP_Text>().text = TimeReq.GetComponent<TMP_InputField>().text;

        }

    }

}
