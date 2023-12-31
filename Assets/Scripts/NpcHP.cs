using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NpcHP : MonoBehaviour
{
    public int maxHP = 100;
    private int HP;
    Animator animator;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(int dmg)
    {
        HP = HP - dmg;
        if (HP <= 0)
        {
            GetComponent<SecurityNPC>().alive = false;
            animator.SetInteger("State", 3);
            agent.enabled = false;
            StartCoroutine(Do());
        }
    }

    IEnumerator Do()
    {
        yield return new WaitForSeconds(2.0f);
        this.gameObject.SetActive(false);
    }
}
