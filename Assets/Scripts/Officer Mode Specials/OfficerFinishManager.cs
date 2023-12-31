using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficerFinishManager : MonoBehaviour
{
    // if robber died -1 ; if robber escaped 1 ; if robber still playing 0 ;
    
    int totalRob = 5;
    public GameObject switchScreen;
    int counter = 0;
    int escaped = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        counter++;
        if(counter == totalRob)
        {
            CheckForAllState();
        }
    }

    public void Escape()
    {
        counter++;
        escaped++;
        if(counter == totalRob)
        {
            CheckForAllState();
        }
    }

    private void CheckForAllState()
    {
        if (escaped == 0)
        {
            // Officer Victory
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("VictoryScene");
        }
        else
        {
            // Office Game Over
            CheckPrevScene.lostCouse = 2;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            switchScreen.gameObject.GetComponent<Animator>().SetBool("State", true);
        }
    }
}
