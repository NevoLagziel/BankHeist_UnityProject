using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LockedDoor : MonoBehaviour
{
    Animator animator;
    public Text lockedDoorText;
    //public Text objTxt;
    public TMP_Text objTxt;
    public GameObject en1;
    public GameObject en2;
    public GameObject en3;
    public bool open;
    AudioSource unlock;

    void Start()
    {
        open = false;
        animator = GetComponent<Animator>();
        unlock = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (open == false)
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>().hasKey)
                {
                    unlock.PlayDelayed(0f);
                    animator.SetBool("State", true);
                    open = true;
                    en1.gameObject.GetComponent<SecurityNPC>().attack = true;
                    en2.gameObject.GetComponent<SecurityNPC>().attack = true;
                    en3.gameObject.GetComponent<SecurityNPC>().attack = true;
                }
                else
                {
                    lockedDoorText.gameObject.SetActive(true);
                    objTxt.text = "Obtain the door key from the security guard";
                }
            }
            else
            {
                animator.SetBool("State", true);
            }
        }else if ((other.gameObject.CompareTag("RoomEnemy") || other.gameObject.CompareTag("TeamMate")) && open == true)
        {
            animator.SetBool("State", true);
        }
        
        //openSound.PlayDelayed(0f);
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>().hasKey)
            {
                animator.SetBool("State", false);
            }
            else
            {
                lockedDoorText.gameObject.SetActive(false);
            }
        }
        else if((other.gameObject.CompareTag("RoomEnemy") || other.gameObject.CompareTag("TeamMate")) && open == true)
        {
            animator.SetBool("State", false);
        }
        //openSound.PlayDelayed(0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
