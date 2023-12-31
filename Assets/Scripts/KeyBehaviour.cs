using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    //public Text objTxt;
    public TMP_Text objTxt;
    public GameObject father;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            father.SendMessage("PlaySound", SendMessageOptions.DontRequireReceiver);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>().hasKey = true;
            objTxt.text = "Grab as much money as you can! \nThen head to the getaway car.";
            this.gameObject.SetActive(false);

        }
    }
}
