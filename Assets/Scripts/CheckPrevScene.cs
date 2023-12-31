using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPrevScene : MonoBehaviour
{
    // Start is called before the first frame update

    public static string prevScene = "";

    public static int lostCouse = 0;

    void Start()
    {
        prevScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
