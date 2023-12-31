using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpGun : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject aCamera;
    public GameObject gunInHand;
    public GameObject gunInBox;
    public Text pickUpText;
    public GameObject ogCrossHair;
    public GameObject pickCrossHair;
    private bool gun;
    //public Text objTxt;
    public TMP_Text objTxt;
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        gun = true;
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (gun == true)
        {
            if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit))
            {
                float distance = Vector3.Distance(transform.position, aCamera.transform.position);
                //checking if the hitted object is this (drawer)
                if (hit.transform.gameObject == this.gameObject)
                {
                    if (distance < 15)
                    {
                        ogCrossHair.SetActive(false);
                        pickCrossHair.SetActive(true);

                        pickUpText.gameObject.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            gunInBox.SetActive(false);
                            gunInHand.SetActive(true);
                            gun = false;
                            sound.PlayDelayed(0f);
                            objTxt.text = "Go and rob the bank.";
                        }
                    }
                }
                else
                {
                    ogCrossHair.SetActive(true);
                    pickCrossHair.SetActive(false);
                    pickUpText.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            ogCrossHair.SetActive(true);
            pickCrossHair.SetActive(false);
            pickUpText.gameObject.SetActive(false);
        }
    }
}
