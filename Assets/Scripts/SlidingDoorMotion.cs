using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlidingDoorMotion : MonoBehaviour
{
    private Animator animator;
    private AudioSource openSound;
    public GameObject zone;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        openSound = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("NPC") || other.gameObject.CompareTag("TeamMate") || other.gameObject.CompareTag("RoomEnemy"))
        {
            if (other.isTrigger == false)
            {
                animator.SetBool("OpenSlidingDoor", true);
                openSound.PlayDelayed(0f);
            }
        }

        if(other.gameObject.CompareTag("Player")) { 
            animator.SetBool("OpenSlidingDoor", true);
            openSound.PlayDelayed(0f);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("NPC") || other.gameObject.CompareTag("TeamMate") || other.gameObject.CompareTag("RoomEnemy"))
        {
            if (other.isTrigger == false)
            {
                animator.SetBool("OpenSlidingDoor", false);
                openSound.PlayDelayed(0f);
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("OpenSlidingDoor", false);
            openSound.PlayDelayed(0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
