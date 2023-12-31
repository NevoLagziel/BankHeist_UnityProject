using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public static int MoneyCounter = 0;
    public Text MoneyFound;
    public GameObject father;
    //public TMP_Text money;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "player")
        {
            father.SendMessage("PlaySound", SendMessageOptions.DontRequireReceiver);
            MoneyCounter++;
            MoneyFound.text = (MoneyCounter * 1000) + "";
            this.gameObject.SetActive(false);
        }
    }
}
