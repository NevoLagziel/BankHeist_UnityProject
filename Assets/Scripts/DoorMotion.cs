using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMotion : MonoBehaviour
{
    Animator animator;
    //private AudioSource openSound;a

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //openSound = GetComponent<AudioSource>();AW
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            animator.SetBool("State", true);
        } else if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("TeamMate")) && other.isTrigger == false)
        {
            animator.SetBool("State", true);
        }
        //openSound.PlayDelayed(0f);
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) { 
            animator.SetBool("State", false);
        }
        else if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("TeamMate")) && other.isTrigger == false)
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
