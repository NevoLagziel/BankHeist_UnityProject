using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public int maxHP = 100;
    public static int HP;
    public TMP_Text HPtxt;
    public bool hasKey;
    public GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        hasKey = false;
        if(SceneManager.GetActiveScene().name == "OfficerScene")
        {
            hasKey = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(int dmg)
    {
        HP = HP - dmg;
        if(HP <= 0)
        {
            HPtxt.text = "HP - "+ 0 + "%";
            CheckPrevScene.lostCouse = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOverScene");
        }
        else
        {
            HPtxt.text = "HP - " + HP + "%";
        }
    }


}
