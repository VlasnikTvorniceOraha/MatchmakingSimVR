using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoScreen : MonoBehaviour
{

    public TMP_Text EloGranica;

    public TMP_Text PingGranica;

    public TMP_Text BrojIgraca;

    public TMP_Text NajboljiPartneri;

    public GameObject TrenutniIgrac;

    public TMP_Dropdown imeIgraca;

    public Transform QueuedPlayers;

    public Transform NonQueuedPlayers;

    private StartQueue Matchmaker;

    public GameObject QueueCanvas;




    // Start is called before the first frame update
    void Start()
    {
        Matchmaker = QueueCanvas.GetComponent<StartQueue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScreen() {

        //Debug.Log("Promijenio vrijednost na " + imeIgraca.captionText.text);

        GameObject trenutniIgrac;

        if (QueuedPlayers.Find(imeIgraca.captionText.text) != null) {
            trenutniIgrac = QueuedPlayers.Find(imeIgraca.captionText.text).gameObject;
        } else if (NonQueuedPlayers.Find(imeIgraca.captionText.text) != null){
            trenutniIgrac = NonQueuedPlayers.Find(imeIgraca.captionText.text).gameObject;
        } else {
            return;
        }

        

        

        PlayerScript skriptaIgraca = trenutniIgrac.GetComponent<PlayerScript>();

        EloGranica.text = "Trenutna Elo Granica: " + (Matchmaker.baseEloDiff + skriptaIgraca.vrijeme * Matchmaker.EloTimeScale);
        PingGranica.text = "Trenutna Ping Granica: " + (Matchmaker.basePingDiff + skriptaIgraca.vrijeme * Matchmaker.PingTimeScale);

        BrojIgraca.text = "Broj igraca u redu: " + (QueuedPlayers.childCount);

        Dictionary<Transform, List<Transform>> MatchmakingDictionary = Matchmaker.EligiblePlayersDict;

        if (MatchmakingDictionary.ContainsKey(trenutniIgrac.transform)) {

            if (MatchmakingDictionary[trenutniIgrac.transform].Count != 0) {
                List<Transform> partneri = MatchmakingDictionary[trenutniIgrac.transform];
                NajboljiPartneri.text = "Najbolji partneri: " + "\n";

                foreach (var partner in partneri) {
                    NajboljiPartneri.text += partner.name + "\n";
                }

            } else {
                NajboljiPartneri.text = "Najbolji partneri: \nNema partnera :(";
            }

            

        } else {
            NajboljiPartneri.text = "Najbolji partneri: Nema partnera :(";
        }


        
    }
}
