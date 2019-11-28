using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public AudioSource HitSource;
    public AudioSource ApplauseSource;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playHitSound(){
        HitSource.Play();
    }

    public void playAplauseSound(){
        ApplauseSource.Play();
    }
}
