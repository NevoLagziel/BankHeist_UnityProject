using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.ConstrainedExecution;
using UnityEditor.Experimental.GraphView;

public class MoneyStolen : MonoBehaviour
{
    public Text amount;
    public Text HP;
    AudioSource audioS;
    public GameObject officerTxt;
    public GameObject robberTxt;
    public GameObject citizenTxt;

    void Start()
    {
        if (CheckPrevScene.prevScene == "OfficerScene")
        {
            officerTxt.SetActive(true);
            HP.text = "HP - " + PlayerHp.HP + "%";
        }
        else if(CheckPrevScene.prevScene == "CitizenScene")
        {
            citizenTxt.SetActive(true);
            HP.gameObject.SetActive(false);
        }
        else
        {
            robberTxt.SetActive(true);
            amount.text = (Money.MoneyCounter * 1000) + "";
            HP.text = "HP - " + PlayerHp.HP + "%";

        }
        
        audioS = GetComponent<AudioSource>();
        audioS.PlayDelayed(0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
