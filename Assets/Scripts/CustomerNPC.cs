using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;
using  UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class CustomerNPC : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    public GameObject player;
    public GameObject gun;
    public bool alive;
    public bool attack = false;
    public GameObject target;
    AudioSource sound;
    string sceneName;
    bool activeSound = false;

    // Start is called before the first frame update
    void Start()
    {
        animator =  GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        sound = GetComponent<AudioSource>();
        sceneName = SceneManager.GetActiveScene().name;
        agent.enabled = true;
        alive = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (alive == true)
        {
            if (attack == true)
            {
                if (!activeSound)
                {
                    activeSound = true;
                    sound.PlayDelayed(0f);
                }
                if (target.CompareTag("Target"))
                {
                    float distance = Vector3.Distance(transform.position, target.transform.position);
                    if (distance > 8)
                    {
                        animator.SetInteger("State", 1);
                        agent.SetDestination(target.transform.position);
                    }
                    else
                    {
                        agent.enabled = false;
                        animator.SetInteger("State", 3);
                    }
                }
                else
                {
                    agent.enabled=false;
                    animator.SetInteger("State", 1);
                }
            }
        }
        else
        {
            agent.enabled = false;
            //GetComponent<Rigidbody>().isKinematic = false;
            animator.SetInteger("State", 2);
            sound.Stop();

            if (sceneName == "OfficerScene")
            {
                CheckPrevScene.lostCouse = 3;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "player")
        {
            if (attack == false)
            {
                if (gun.gameObject.activeSelf)
                {
                    activeSound = true;
                    sound.PlayDelayed(0f);
                    attack = true;
                }
            }
        }
    }


}
