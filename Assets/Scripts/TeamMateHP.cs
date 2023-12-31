using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class TeamMateHP : MonoBehaviour
{
    public int maxHP = 100;
    private int HP;
    Animator animator;
    NavMeshAgent agent;
    public TMP_Text HPtxt;

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
        HP = HP - (dmg*2);

        if (HP <= 0)
        {
            HPtxt.text = "Teammate died";
            GetComponent<TeamMateNPC>().alive = false;
            animator.SetInteger("State", 3);
            agent.enabled = false;
            StartCoroutine(Do());
        }else
        {
            HPtxt.text = "Teammate HP - " + HP + "%";
        }

    }

    IEnumerator Do()
    {
        yield return new WaitForSeconds(2.0f);
        this.gameObject.SetActive(false);
    }
}
