using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RobberNPCHP : MonoBehaviour
{
    public int maxHP = 100;
    private int HP;
    Animator animator;
    NavMeshAgent agent;

    public Text moneyLost;

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
        HP = HP - (dmg * 2);

        if (HP <= 0)
        {
            if (GetComponent<RobberNPCScript>().alive == true)
            {
                GetComponent<RobberNPCScript>().alive = false;
                Money.MoneyCounter -= 20;
                moneyLost.text = "- " + (Money.MoneyCounter * 1000);
                animator.SetInteger("State", 3);
                agent.enabled = false;
                StartCoroutine(Do());
            }
        }
    }

    IEnumerator Do()
    {
        yield return new WaitForSeconds(2.0f);
        GameObject.FindGameObjectWithTag("Robbers").GetComponent<OfficerFinishManager>().Die();
        this.gameObject.SetActive(false);
    }
}
