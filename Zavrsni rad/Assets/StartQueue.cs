using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class StartQueue : MonoBehaviour
{

    public GameObject QSign;

    private TMP_Text QSignRenderer;

    private bool started;

    public GameObject QueuedPlayers;

    public GameObject NonQueuedPlayers;

    public GameObject BLUtim;

    public GameObject REDtim;

    public GameObject InfoScreen;

    private InfoScreen skriptaInfoScreen;
    // Start is called before the first frame update
    void Start()
    {
        started = false;
        QSignRenderer = QSign.GetComponent<TMP_Text>();
        skriptaInfoScreen = InfoScreen.GetComponent<InfoScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQ()
    {
        
        if (started) {
            CancelInvoke("Matchmaker");
            //CAncelInvoke za Q
            started = false;
            QSignRenderer.faceColor = Color.red;
            CancelInvoke("TimeTick");
        }
        else {
            InvokeRepeating("Matchmaker", 0, 2.0f);
            //Invokerepeating Q
            started = true;
            QSignRenderer.faceColor = Color.green;
            InvokeRepeating("TimeTick", 0, 1.0f);
            

        }

    }

    

    public float EloTimeScale = 1;
    public float PingTimeScale = 1;

    public int baseEloDiff = 30;
    public int basePingDiff = 50;

    public int brojIgracaUTimu = 1;

    public Dictionary<Transform, List<Transform>> EligiblePlayersDict = new Dictionary<Transform, List<Transform>>();

    public GameObject MatchInstance;

    public int timeRequirementForMatch = 6;


    void TimeTick() 
    {

        skriptaInfoScreen.UpdateScreen();

        foreach (Transform trans in QueuedPlayers.transform) {
                
                PlayerScript dummyScript = trans.GetComponent<PlayerScript>();

                dummyScript.vrijeme += 1;
                dummyScript.VrijemeText.text = "Vrijeme: " + dummyScript.vrijeme;
                
            }
        

        

    }

    void Matchmaker() 
    {
        //ELigibility provjera
        foreach (Transform trans1 in QueuedPlayers.transform) {

            //lista igraca koji se mogu staviti u match sa trenutnim igracem
            List<Transform> EligiblePlayers = new List<Transform>();
            
            foreach (Transform trans2 in QueuedPlayers.transform) {

                if (trans1 != trans2) {

                    //mozda adjustati base modifiere ovisno o drugim faktorima prije provjera
                    //izvuci par igraca
                    var player1 = trans1.GetComponent<PlayerScript>();
                    var player2 = trans2.GetComponent<PlayerScript>();

                    bool eligible = true;

                    //provjera visokog pinga
                    if (player1.ping > 150) {
                        Debug.Log(trans1.name + " ima visoko kanjenje obilaska, preusmjeravanje na drugi server");
                        trans1.SetParent(NonQueuedPlayers.transform);
                    }

                    if (player2.ping > 150) {
                        Debug.Log(trans2.name + " ima visoko kanjenje obilaska, preusmjeravanje na drugi server");
                        trans2.SetParent(NonQueuedPlayers.transform);
                    }

                    //provjera razlike eloa
                    var razlikaElo = Mathf.Abs(player1.Elo - player2.Elo);

                    if (razlikaElo > (baseEloDiff + player1.vrijeme * EloTimeScale)) {
                        eligible = false;

                    }

                    //provjera razlike pinga
                    var razlikaPing = Mathf.Abs(player1.ping  - player2.ping);

                    if (razlikaPing > (basePingDiff + player1.vrijeme * PingTimeScale)) {
                        eligible = false;

                    }

                    //ako je provjera prosla onda stavi player2 u eligible listu za playera1 i idi dalje

                    if (eligible) {
                        Debug.Log(trans1.name + " and " + trans2.name + " are eligible for matchmaking!");
                        EligiblePlayers.Add(trans2);
                    }


                }

            }


            //stavi izracunate liste u dictionary i idi dalje

            if (EligiblePlayersDict.ContainsKey(trans1) == false) {

                if (EligiblePlayers != null) {
                    EligiblePlayersDict.Add(trans1, EligiblePlayers);
                } else {
                    EligiblePlayersDict.Add(trans1, null);
                }
            } else {
                var values = EligiblePlayersDict[trans1];
                if (values == null) {
                    EligiblePlayersDict[trans1] = EligiblePlayers;
                } else {
                    var finalnaLista = values.Union(EligiblePlayers).ToList();
                    EligiblePlayersDict[trans1] = finalnaLista;
                }

            }

            
        }
        //printeraj
        foreach (var igraci in EligiblePlayersDict) {
            Debug.Log("Trenutni igrac: " + igraci.Key.name);
            if (igraci.Value != null) {
                foreach (var ime in igraci.Value) {
                    Debug.Log(ime.name);
                }
            } else {
                Debug.Log("Tužno!");
            }
            Debug.Log("  ");
        }
        
        //sada treba odrediti koji eligible igraci ce se zapravo matchati

        foreach (var player1 in EligiblePlayersDict) {

            if (player1.Value.Count > 0) {

                int najboljiPartnerIndex = 0;
                int najveciKoeficijent= 0;

                foreach (var player2 in player1.Value) {

                    int elo1 = player1.Key.GetComponent<PlayerScript>().Elo;
                    int elo2 = player2.GetComponent<PlayerScript>().Elo;
                    int koeficijentElo;
                    
                    if (elo1 >= elo2) {
                        koeficijentElo = elo2/elo1;
                    } else {
                        koeficijentElo = elo1/elo2;
                    }

                    if (koeficijentElo > najveciKoeficijent) {
                        najveciKoeficijent = koeficijentElo;
                        najboljiPartnerIndex = player1.Value.IndexOf(player2);
                    }
                }

                Debug.Log("Najbolji partner za " + player1.Key.name + " je " + player1.Value[najboljiPartnerIndex].name);

                int zbrojVremena = player1.Key.GetComponent<PlayerScript>().timeSpentEligible + player1.Value[najboljiPartnerIndex].GetComponent<PlayerScript>().timeSpentEligible;

                if (zbrojVremena >= timeRequirementForMatch) {

                    //Matchaj ekipu
                    player1.Key.SetParent(REDtim.transform);
                    player1.Key.position = REDtim.transform.position;

                    player1.Value[najboljiPartnerIndex].SetParent(BLUtim.transform);
                    player1.Value[najboljiPartnerIndex].position = BLUtim.transform.position;

                    
                    //EligiblePlayersDict.Remove(player1.Value[najboljiPartnerIndex]);
                    //EligiblePlayersDict.Remove(player1.Key);


                } else {
                    player1.Key.GetComponent<PlayerScript>().timeSpentEligible += 1;
                    player1.Value[najboljiPartnerIndex].GetComponent<PlayerScript>().timeSpentEligible += 1;
                }

            }

            

        }

    }
}


