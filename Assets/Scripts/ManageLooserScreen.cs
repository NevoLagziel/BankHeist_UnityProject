using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageLooserScreen : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject dead;
    public GameObject escape;
    public Text amountStolen;
    public GameObject citizen;

    void Start()
    {
        switch (CheckPrevScene.lostCouse)
        {
            case 1:
                dead.SetActive(true);
                break;

            case 2:
                escape.SetActive(true);
                amountStolen.text = (Money.MoneyCounter * 1000) + "";
                break;

            case 3:
                citizen.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
