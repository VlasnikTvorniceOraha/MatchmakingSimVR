using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    public GameObject player;

    private GameObject playerClone;

    public Transform NonQueuedPlayers;

    public Transform location;

    private List<GameObject> klonovi;

    public int SpawnCounter = 1;

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
        playerClone = Instantiate(player);
        playerClone.transform.SetParent(NonQueuedPlayers, false);
        playerClone.transform.position = location.transform.position;
        //Instantiate(player, parent, false);
        playerClone.name = SpawnCounter.ToString();
        SpawnCounter += 1;
        
        location.transform.position = location.transform.position + new Vector3(-2, 0, 0);
        
        

        playerClone.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);

        //klonovi.Add(playerClone);




    }
}
