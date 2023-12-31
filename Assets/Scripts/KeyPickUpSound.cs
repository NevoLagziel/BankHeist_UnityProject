using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUpSound : MonoBehaviour
{
    AudioSource pickUpSound;
    // Start is called before the first frame update
    void Start()
    {
        pickUpSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound()
    {
        pickUpSound.PlayDelayed(0f);
    }
}
