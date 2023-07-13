using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpcijeNaivni : MonoBehaviour
{

    public GameObject queue;
    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        StartQueue qSkripta = queue.GetComponent<StartQueue>();
        Transform granicaElo = self.transform.Find("EloGranicaTimova");
        granicaElo.Find("box").GetComponent<TMP_Text>().text = qSkripta.NaiveEloDiff.ToString();

        Transform brojIgraca = self.transform.Find("BrojIgraca");
        brojIgraca.Find("box").GetComponent<TMP_Text>().text = qSkripta.brojIgracaUTimu.ToString();

        Transform brojPokusaja = self.transform.Find("BrojPokusaja");
        brojPokusaja.Find("box").GetComponent<TMP_Text>().text = qSkripta.brojPokusaja.ToString();
 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject granicaEloNaivna;

    public GameObject BrojIgraca;

    public GameObject BrojPokusaja;

    public void Change() 
    {
        //Debug.Log(EloTextInput.GetComponent<TMP_InputField>().text);

        StartQueue qSkripta = queue.GetComponent<StartQueue>();



        if (granicaEloNaivna.GetComponent<TMP_InputField>().text != "") {

            qSkripta.NaiveEloDiff = int.Parse(granicaEloNaivna.GetComponent<TMP_InputField>().text);
            Transform granicaElo = self.transform.Find("EloGranicaTimova");
            granicaElo.Find("box").GetComponent<TMP_Text>().text = granicaEloNaivna.GetComponent<TMP_InputField>().text;

        }

        if (BrojIgraca.GetComponent<TMP_InputField>().text != "") {

            qSkripta.brojIgracaUTimu = int.Parse(BrojIgraca.GetComponent<TMP_InputField>().text);
            Transform brojIgraca = self.transform.Find("BrojIgraca");
            brojIgraca.Find("box").GetComponent<TMP_Text>().text = BrojIgraca.GetComponent<TMP_InputField>().text;

        }

        if (BrojPokusaja.GetComponent<TMP_InputField>().text != "") {

            qSkripta.brojPokusaja = int.Parse(BrojPokusaja.GetComponent<TMP_InputField>().text);
            Transform brojPokusaja = self.transform.Find("BrojPokusaja");
            brojPokusaja.Find("box").GetComponent<TMP_Text>().text = BrojPokusaja.GetComponent<TMP_InputField>().text;

        }



    }
}
