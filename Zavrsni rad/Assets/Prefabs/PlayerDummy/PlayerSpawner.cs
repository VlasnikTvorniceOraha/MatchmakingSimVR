using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerSpawner : MonoBehaviour
{
    

    public GameObject playerPrefab;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Debug.Log("Buraz");
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        foreach (var item in devices) {
            Debug.Log(item.name);
        }


    }


    // Update is called once per frame
    void Update()
    {
        
        

    }
}
