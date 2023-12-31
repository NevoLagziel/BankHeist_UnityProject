using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSoundScript : MonoBehaviour
{
    AudioSource alarmSound;

    // Start is called before the first frame update
    void Start()
    {
        alarmSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlaySound()
    {
        alarmSound.PlayDelayed(0f);
    }
}
