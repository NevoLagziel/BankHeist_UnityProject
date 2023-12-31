using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalFinish : MonoBehaviour
{
    public GameObject switchScreen;
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
        if (other.gameObject.CompareTag("Player"))
        {
            switchScreen.gameObject.GetComponent<Animator>().SetBool("State",true);
        }
    }

}
