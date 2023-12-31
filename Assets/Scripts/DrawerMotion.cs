using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class DrawerMotion : MonoBehaviour
{
    public GameObject aCamera;
    public GameObject ogCrossHair;
    public GameObject pickCrossHair;
    public Text openDrawerText;
    public Text closeDrawerText;
    public GameObject chest;
    private bool open = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = chest.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit))
        {
            float distance = Vector3.Distance(transform.position, aCamera.transform.position);
            //checking if the hitted object is this (drawer)
            if (hit.transform.gameObject == this.gameObject)
            {
                ogCrossHair.SetActive(false);
                pickCrossHair.SetActive(true);

                if (distance < 15)
                {
                    if(open == false)
                    {
                        openDrawerText.gameObject.SetActive(true);
                    }else
                    {
                        closeDrawerText.gameObject.SetActive(true);
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (open == false)
                        {
                            animator.SetBool("Status", true);
                            open = true;
                            openDrawerText.gameObject.SetActive(false);
                        }
                        else
                        {
                            animator.SetBool("Status", false);
                            open = false;
                            closeDrawerText.gameObject.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                ogCrossHair.SetActive(true);
                pickCrossHair.SetActive(false);
                openDrawerText.gameObject.SetActive(false);
                closeDrawerText.gameObject.SetActive(false);
            }
        }
    }
}
