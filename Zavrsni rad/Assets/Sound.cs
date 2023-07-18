using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioSource source;
    public AudioClip clip;

    public void playSound() {

        source.clip = clip;
        source.Play();
    }

    public AudioClip clip2;

    public void playSound2() {

        source.clip = clip2;
        source.Play();
    }

    public AudioClip clip3;

    public void playSound3() {

        source.clip = clip3;
        source.Play();
    }
}
