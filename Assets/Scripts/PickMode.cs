using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAsCriminal()
    {
        Money.MoneyCounter = 0;
        SceneManager.LoadScene("SampleScene");
    }

    public void StartAsPolice()
    {
        Money.MoneyCounter = 0;
        SceneManager.LoadScene("OfficerScene");
    }

    public void StartAsCitizen()
    {
        Money.MoneyCounter = 0;
        SceneManager.LoadScene("CitizenScene");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
