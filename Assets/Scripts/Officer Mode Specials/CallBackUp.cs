using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CallBackUp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject backUp;
    public TMP_Text txt;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            txt.text = "Backup is on the way";
            backUp.SetActive(true);
            StartCoroutine(Do());
        }
    }

    IEnumerator Do()
    {
        yield return new WaitForSeconds(2f);
        txt.gameObject.SetActive(false);
    }
}
