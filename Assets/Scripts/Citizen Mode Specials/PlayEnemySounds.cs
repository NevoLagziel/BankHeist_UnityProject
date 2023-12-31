using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEnemySounds : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource gaspSound;
    void Start()
    {
        gaspSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        gaspSound.PlayDelayed(0f);
    }
}
