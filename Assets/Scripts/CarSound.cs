using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    AudioSource driftSound;
    // Start is called before the first frame update
    void Start()
    {
        driftSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Drift()
    {
        driftSound.PlayDelayed(0f);
    }

    public void StopDrift()
    {
        driftSound.Stop();
    }

}
