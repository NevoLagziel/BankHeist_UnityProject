using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoneySound : MonoBehaviour
{

    private AudioSource collectSound;
    public GameObject backUp;
    public GameObject backUpFar;
    public Text beckUpTxt;
    bool backUpCalled;

    // Start is called before the first frame update
    void Start()
    {
        backUpCalled = false;
        collectSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound() {
        collectSound.PlayDelayed(0f);
        if(Money.MoneyCounter >= 15)
        {
            if (backUpCalled == false)
            {
                backUpCalled = true;
                backUp.SetActive(true);
                StartCoroutine(Do());
            }
        }
    }

    IEnumerator Do()
    {
        beckUpTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        backUpFar.SetActive(true);
        beckUpTxt.gameObject.SetActive(false);
    }

}
