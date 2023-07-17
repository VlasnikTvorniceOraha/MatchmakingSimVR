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

    private string TipReda = "Normalni";
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
            
            started = false;
            QSignRenderer.faceColor = Color.red;
            CancelInvoke("TimeTick");

            if (TipReda == "Normalni") {

                CancelInvoke("Matchmaker");
                //CAncelInvoke za Q
            } else if (TipReda == "Naivni") {
                CancelInvoke("NaiveMatchmaker");
                //CAncelInvoke za Q
            }
        }
        else {
            
            started = true;
            QSignRenderer.faceColor = Color.green;
            InvokeRepeating("TimeTick", 0, 1.0f);

            if (TipReda == "Normalni") {

                InvokeRepeating("Matchmaker", 0, 3.0f);
                //Invokerepeating Q
            } else if (TipReda == "Naivni") {
                InvokeRepeating("NaiveMatchmaker", 0, 3.0f);
            }


        }

    }

    

    public float EloTimeScale = 1;
    public float PingTimeScale = 1;

    public int baseEloDiff = 30;
    public int basePingDiff = 50;

   

    public Dictionary<Transform, List<Transform>> EligiblePlayersDict = new Dictionary<Transform, List<Transform>>();

    public int timeRequirementForMatch = 10;

    


    void TimeTick() 
    {


        if (TipReda == "Normalni") {
            skriptaInfoScreen.UpdateScreen();
            UpdateVis();
        }
        

        foreach (Transform trans in QueuedPlayers.transform) {
                
                PlayerScript dummyScript = trans.GetComponent<PlayerScript>();

                dummyScript.vrijeme += 1;
                dummyScript.VrijemeText.text = "Vrijeme: " + dummyScript.vrijeme;
                
            }
        

        

    }

    public TMP_Text TipRedaText;

    public GameObject OpcijeReda1v1;

    public GameObject OpcijeRedaNaivni;

    public void PromijeniTipReda() {


        if (!started) {
            if (TipReda == "Normalni") {
                TipReda = "Naivni";
                TipRedaText.text = "Naivni";
                OpcijeReda1v1.SetActive(false);
                OpcijeRedaNaivni.SetActive(true);
            } else {
                TipReda = "Normalni";
                TipRedaText.text = "1v1";
                OpcijeReda1v1.SetActive(true);
                OpcijeRedaNaivni.SetActive(false);
            }

        }

        

    }

    public int NaiveEloDiff = 200;

    public int brojIgracaUTimu = 2;

    public int brojPokusaja = 3;

    

    void NaiveMatchmaker() {

        List<Transform> sviIgraci = new List<Transform>();
        List<Transform> odabraniIgraci = new List<Transform>();
        List<Transform> tim1 = new List<Transform>();
        List<Transform> tim2 = new List<Transform>();
        int tim1Elo = 0;
        int tim2Elo = 0;

        if (QueuedPlayers.transform.childCount >= brojIgracaUTimu * 2) {

            foreach (Transform trans in QueuedPlayers.transform) {
                sviIgraci.Add(trans);
            }

            //randomly odabrati igrace
            for (int b = 0; b < brojIgracaUTimu * 2; b++) {

                int randomBirac = Random.Range(0, sviIgraci.Count);

                odabraniIgraci.Add(sviIgraci[randomBirac]);
                sviIgraci.RemoveAt(randomBirac);

            }

            //Debug.Log(odabraniIgraci.Count);
            //odabrati 2 najjacih igraca i smijestiti ih u svaki tim

            for (int i = 0; i < 2; i++) {

                int najveciElo = 0;
                int najveciIndeks = 0;

                foreach (Transform trans in odabraniIgraci) {

                    PlayerScript skripta = trans.GetComponent<PlayerScript>();

                    if (skripta.Elo > najveciElo) {
                        najveciElo = skripta.Elo;
                        najveciIndeks = odabraniIgraci.IndexOf(trans);
                    }
                }

                if (i == 0) {
                    tim1.Add(odabraniIgraci[najveciIndeks]);
                    odabraniIgraci.RemoveAt(najveciIndeks);
                    tim1Elo += najveciElo;
                } else {
                    tim2.Add(odabraniIgraci[najveciIndeks]);
                    odabraniIgraci.RemoveAt(najveciIndeks);
                    tim2Elo += najveciElo;
                }

            }

            //dopuni timove sa random igracima i odredi hoce li match proci

            for (int i = 0; i < brojPokusaja; i++) {

                while (odabraniIgraci.Count != 0) {

                    int randomBirac = Random.Range(0, odabraniIgraci.Count);

                    PlayerScript skripta = odabraniIgraci[randomBirac].GetComponent<PlayerScript>();

                    if (tim1.Count < brojIgracaUTimu) {
                        tim1.Add(odabraniIgraci[randomBirac]);
                        odabraniIgraci.RemoveAt(randomBirac);
                        tim1Elo += skripta.Elo;

                    } else {
                        tim2.Add(odabraniIgraci[randomBirac]);
                        odabraniIgraci.RemoveAt(randomBirac);
                        tim2Elo += skripta.Elo;

                    }

                }

                //sada kada imamo timove procijeni njihov average elo i razliku eloa
                //Debug.Log(tim1Elo);
                //Debug.Log(tim2Elo);
                int ukupnaRazlikaElo = Mathf.Abs((tim1Elo / brojIgracaUTimu) - (tim2Elo / brojIgracaUTimu));

                if (ukupnaRazlikaElo < NaiveEloDiff) {
                    Debug.Log("Mec prihvacen");
                    MakeMatch(tim1, tim2);
                    i = brojPokusaja;
                } else {
                    Debug.Log("Prevelika razlika, mec odbacen");
                }

            }

            
            

        } else {
            Debug.Log("Premalo igraca u redu");
        }



    }


    public GameObject Visualizer;
    public void UpdateVis() {
        
        foreach (Transform trans in QueuedPlayers.transform) {
                
                PlayerScript dummyScript = trans.GetComponent<PlayerScript>();

                dummyScript.visDummy.transform.localPosition = new Vector3(((dummyScript.Elo - 100) / 40) - 10, ((dummyScript.basePing - 10) / 10) - 5, 0);

                if (TipReda == "Normalni") {


                    Transform backdrop = dummyScript.visDummy.transform.Find("DummyBackdrop");

                    MeshRenderer backdropRenderer = backdrop.GetComponent<MeshRenderer>();
                    

                    if (!EligiblePlayersDict.ContainsKey(trans)) {
                        backdropRenderer.material.color = new Vector4(1f, 0f, 0f, 0.4f);

                    } else {

                        var values = EligiblePlayersDict[trans];
                        if (values == null) {
                            backdropRenderer.material.color = new Vector4(1f, 0f, 0f, 0.4f);
                        } else {
                            backdropRenderer.material.color = new Vector4(0f, 1f, 0f, 0.4f);
                        }
                    }

                    

                    backdrop.localScale = new Vector3((baseEloDiff + dummyScript.vrijeme * EloTimeScale) *  (float)0.11125, (basePingDiff + dummyScript.vrijeme * PingTimeScale) * (float)0.45045, 0);

                } else {

                    Transform backdrop = dummyScript.visDummy.transform.Find("DummyBackdrop");

                    backdrop.localScale = new Vector3(0.45f, 0.45f, 0.45f);

                    MeshRenderer backdropRenderer = backdrop.GetComponent<MeshRenderer>();
                    backdropRenderer.material.color = new Vector4(1f, 0f, 0f, 0.4f);

                }
                
                
            }

        foreach (Transform trans in NonQueuedPlayers.transform) {
                
                PlayerScript dummyScript = trans.GetComponent<PlayerScript>();

                dummyScript.visDummy.transform.localPosition = new Vector3(((dummyScript.Elo - 100) / 40) - 10, ((dummyScript.basePing - 10) / 10) - 5, 0);

                if (TipReda == "Normalni") {

                    Transform backdrop = dummyScript.visDummy.transform.Find("DummyBackdrop");

                    backdrop.localScale = new Vector3((baseEloDiff + dummyScript.vrijeme * EloTimeScale) *  (float)0.11125, (basePingDiff + dummyScript.vrijeme * PingTimeScale) * (float)0.45045, 0);

                    MeshRenderer backdropRenderer = backdrop.GetComponent<MeshRenderer>();
                    

                    if (!EligiblePlayersDict.ContainsKey(trans)) {
                        backdropRenderer.material.color = new Vector4(1f, 0f, 0f, 0.4f);

                    } else {

                        var values = EligiblePlayersDict[trans];
                        if (values == null) {
                            backdropRenderer.material.color = new Vector4(1f, 0f, 0f, 0.4f);
                        } else {
                            backdropRenderer.material.color = new Vector4(0f, 1f, 0f, 0.4f);
                        }
                    }
                } else {

                    Transform backdrop = dummyScript.visDummy.transform.Find("DummyBackdrop");

                    backdrop.localScale = new Vector3((float)0.45, (float)0.45, (float)0.45);
                    MeshRenderer backdropRenderer = backdrop.GetComponent<MeshRenderer>();
                    backdropRenderer.material.color = new Vector4(1f, 0f, 0f, 0.4f);
                }
                
                
            }
        
    }
    
    public GameObject beamOfLight;

    public Transform MatchingPlayers;

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
                    /*if (player1.ping > 150) {
                        Debug.Log(trans1.name + " ima visoko kanjenje obilaska, preusmjeravanje na drugi server");
                        trans1.SetParent(NonQueuedPlayers.transform);
                    }

                    if (player2.ping > 150) {
                        Debug.Log(trans2.name + " ima visoko kanjenje obilaska, preusmjeravanje na drugi server");
                        trans2.SetParent(NonQueuedPlayers.transform);
                    }*/

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
                    List<Transform> tim1 = new List<Transform>();
                    List<Transform> tim2 = new List<Transform>();
                    tim1.Add(player1.Key);
                    tim2.Add(player1.Value[najboljiPartnerIndex]);

                    player1.Key.transform.SetParent(MatchingPlayers);
                    player1.Value[najboljiPartnerIndex].transform.SetParent(MatchingPlayers);

                    PlayerScript p1Script = player1.Key.GetComponent<PlayerScript>();
                    PlayerScript p2Script = player1.Value[najboljiPartnerIndex].GetComponent<PlayerScript>();

                    EligiblePlayersDict.Remove(player1.Value[najboljiPartnerIndex]);
                    EligiblePlayersDict.Remove(player1.Key);

                    Destroy(p1Script.visDummy);
                    Destroy(p2Script.visDummy);

                    StartCoroutine(Animation(tim1, tim2));

                    
                    

                    


                } else {
                    player1.Key.GetComponent<PlayerScript>().timeSpentEligible += 1;
                    player1.Value[najboljiPartnerIndex].GetComponent<PlayerScript>().timeSpentEligible += 1;
                }

            }

            

        }

    }


    public GameObject MatchInstance;
    public Transform MatchLocation;

    public Transform Matches;

    void MakeMatch(List<Transform> plaviTim, List<Transform> crveniTim) {

        GameObject match = Instantiate(MatchInstance);
        match.transform.SetParent(Matches, false);

        match.transform.position = MatchLocation.transform.position;

        MatchLocation.localPosition = MatchLocation.localPosition + new Vector3(0, 0, 8);

        Transform BluTim = match.transform.Find("BLU tim");
        Transform RedTim = match.transform.Find("RED tim");

        foreach (var trans in plaviTim) {

            trans.SetParent(match.transform);
            trans.position = BluTim.position;
            trans.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
            BluTim.localPosition = BluTim.localPosition + new Vector3(0, 0, -1);

            Transform kapsula = trans.Find("Capsule");

            MeshRenderer renderer = kapsula.GetComponent<MeshRenderer>();
            renderer.material.color = Color.blue;

            Destroy(trans.Find("beam").gameObject);
            

        }

        foreach (var trans in crveniTim) {

            trans.SetParent(match.transform);
            trans.position = RedTim.position;
            RedTim.localPosition = RedTim.localPosition + new Vector3(0, 0, -1);

            Transform kapsula = trans.Find("Capsule");
            MeshRenderer renderer = kapsula.GetComponent<MeshRenderer>();
            renderer.material.color = Color.red;

            Destroy(trans.Find("beam").gameObject);

        }



    }

    IEnumerator Animation(List<Transform> plaviTim, List<Transform> crveniTim) {

        //animacija svijetla

        BeamSpawner(plaviTim, crveniTim);
        RemoveFromDropdown(plaviTim, crveniTim);

        yield return new WaitForSeconds(10);

        //matchmaking
        MakeMatch(plaviTim, crveniTim);

        
    }

    void BeamSpawner(List<Transform> plaviTim, List<Transform> crveniTim) {

        foreach(Transform trans in plaviTim) {

            GameObject beam = Instantiate(beamOfLight);
            beam.transform.SetParent(trans);
            beam.transform.localPosition = new Vector3(1.5f, 1.5f, -1.5f);
            beam.name = "beam";

        }

        foreach(Transform trans in crveniTim) {

            GameObject beam = Instantiate(beamOfLight);
            beam.transform.SetParent(trans);
            beam.transform.localPosition = new Vector3(1.5f, 1.5f, -1.5f);
            beam.name = "beam";


        }
    }

    public TMP_Dropdown igraciDropdown;

    void RemoveFromDropdown(List<Transform> plaviTim, List<Transform> crveniTim) {

        foreach (Transform trans in plaviTim) {

 

            for (int i = 0; i < igraciDropdown.options.Count; i++) {

                if (igraciDropdown.options[i].text == trans.name) {
                    igraciDropdown.options.RemoveAt(i);
                }
            }

        }

        foreach (Transform trans in crveniTim) {

 

            for (int i = 0; i < igraciDropdown.options.Count; i++) {

                if (igraciDropdown.options[i].text == trans.name) {
                    igraciDropdown.options.RemoveAt(i);
                }
            }

        }

    }
}




