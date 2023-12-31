using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundEffect : MonoBehaviour
{
    // Start is called before the first frame update

    AudioSource hitSound;

    private void Awake()
    {
        hitSound = GetComponent<AudioSource>();
        StartCoroutine(PlaySound());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlaySound()
    {
        hitSound.PlayDelayed(0f);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        
    }
}
