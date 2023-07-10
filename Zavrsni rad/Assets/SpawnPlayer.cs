using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnPlayer : MonoBehaviour
{

    public GameObject player;

    private GameObject playerClone;

    private GameObject visualizerClone;

    public Transform NonQueuedPlayers;

    public Transform location;

    private List<GameObject> klonovi;

    public int SpawnCounter = 1;

    public TMP_Dropdown igraciDropdown;

    public GameObject VisualizerDummy;

    public GameObject Visualizer;


    // Start is called before the first frame update
    void Start()
    {
        klonovi = new List<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void Stvori() 
    {
        //instanciraj igraca
        playerClone = Instantiate(player);
        playerClone.transform.SetParent(NonQueuedPlayers, false);
        
        //Instantiate(player, parent, false);
        playerClone.name = "Igrac " + SpawnCounter.ToString();
        SpawnCounter += 1;
        
        //pronadi mjesto za igraca
        
        

        playerClone.transform.position = location.transform.position;
        location.transform.position = location.transform.position + new Vector3(-2, 0, 0);
        
        

        playerClone.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);

        //klonovi.Add(playerClone);

        //dodaj ga na dropdown igraca

        //TMP_Dropdown.OptionData playerOption;


        List<string> opcije = new List<string>();

        opcije.Add(playerClone.name);

        igraciDropdown.AddOptions(opcije);

        //Stvaranje visualizer dummya

        visualizerClone = Instantiate(VisualizerDummy);

        visualizerClone.transform.SetParent(Visualizer.transform, false);

        PlayerScript skripta = playerClone.GetComponent<PlayerScript>();

        skripta.visDummy = visualizerClone;
        
        //visualizerClone.transform.localPosition = new Vector3(((skripta.Elo - 100) / 40) - 10, ((skripta.ping - 10) / 10) - 5, 0);
        //visualizerClone.transform.localPosition.Set(((skripta.Elo - 100) / 40) - 10, 0, ((skripta.ping - 10) / 10) - 5);
    }
}
