using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class QueueToggle : MonoBehaviour
{
    public GameObject dummy;

    public GameObject toggle;
    
    public GameObject capsule;

    private MeshRenderer capsuleRenderer;

    private bool ticked;

    public GameObject NonQueuedPlayers;

    public GameObject QueuedPlayers;

    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    void Start()
    {
        ticked = false;
        capsuleRenderer = capsule.GetComponent<MeshRenderer>();
        QueuedPlayers = GameObject.FindGameObjectWithTag("QPlayers");
        NonQueuedPlayers = GameObject.FindGameObjectWithTag("NQPlayers");

    }

    public void ToggleQ() 
    {
        

        if (ticked) {
            //Debug.Log("Stigara False");
            ticked = false;
            capsuleRenderer.material.color = Color.red;
            dummy.transform.SetParent(NonQueuedPlayers.transform);
        }
        else {
            //Debug.Log("Stigara True");
            ticked = true;
            capsuleRenderer.material.color = Color.green;
            dummy.transform.SetParent(QueuedPlayers.transform);
        }

    }
}
