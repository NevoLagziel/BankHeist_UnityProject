using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAttackPlayer : MonoBehaviour
{
    Animator animator;
    private NavMeshAgent agent;
    public GameObject projectilePrefab;
    public Transform shootPoint;

    public GameObject player;
    public GameObject target1;
    public GameObject target2;
    GameObject lastTarget;

    public GameObject dontMove;

    public bool attack;
    public bool alive;
    int counter = 0;
    public Text lines;
    bool checking = false;


    public float shootInterval = 1f;
    public float projectileSpeed = 20f;
    private float rotationSpeed = 5f;
    private float lastShootTime;
    public GameObject NPCGun;
    public GameObject NPCAllGun;
    public GameObject runningGun;
    AudioSource gunSound;

    public GameObject head;
    public bool cantShoot = false;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        gunSound = GetComponent<AudioSource>();

        NPCAllGun.SetActive(false);
        agent.enabled = true;
        runningGun.SetActive(true);
        animator.SetInteger("State", 1);
        agent.SetDestination(target1.transform.position);
        lastTarget = target1;

        alive = true;
        attack = false;
    }

    void Update()
    {

        if (attack == false)
        {
            if (checking == false)
            {
                float distance1 = Vector3.Distance(transform.position, target1.transform.position);
                float distance2 = Vector3.Distance(transform.position, target2.transform.position);


                if (distance1 < 6)
                {
                    NPCAllGun.SetActive(false);
                    agent.enabled = true;
                    runningGun.SetActive(true);
                    animator.SetInteger("State", 1);
                    agent.SetDestination(target2.transform.position);
                    lastTarget = target2;
                }
                else if (distance2 < 6)
                {
                    NPCAllGun.SetActive(false);
                    agent.enabled = true;
                    runningGun.SetActive(true);
                    animator.SetInteger("State", 1);
                    agent.SetDestination(target1.transform.position);
                    lastTarget = target1;
                }

                RaycastHit hit;
                if (Physics.Raycast(head.transform.position, head.transform.forward, out hit, Mathf.Infinity, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        CheckIfPlayerVis();
                    }
                }

            }

        }
        else
        {
            float playerDistance = Vector3.Distance(transform.position, player.transform.position);
            float hightDiff = Mathf.Abs(transform.position.y - player.transform.position.y);

            if ((playerDistance > 45) || hightDiff > 3)
            {
                agent.enabled = true;
                animator.SetInteger("State", 1);
                NPCAllGun.SetActive(false);
                runningGun.SetActive(true);
                agent.SetDestination(player.transform.position);
            }
            else
            {
                if (cantShoot == false)
                {
                    Vector3 rayOrigin = head.transform.position;
                    Vector3 directionToEnemy = player.transform.position - rayOrigin;

                    RaycastHit hit;
                    if (Physics.Raycast(rayOrigin, directionToEnemy, out hit, directionToEnemy.magnitude, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                    {
                        if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("door") || hit.collider.CompareTag("TeamMate"))
                        {
                            agent.enabled = true;
                            animator.SetInteger("State", 1);
                            NPCAllGun.SetActive(false);
                            runningGun.SetActive(true);
                            agent.SetDestination(player.transform.position);
                        }
                        else
                        {
                            agent.enabled = false;
                            animator.SetInteger("State", 2);
                            NPCAllGun.SetActive(true);
                            runningGun.SetActive(false);
                            FireGun();
                        }
                    }
                    else
                    {
                        agent.enabled = false;
                        animator.SetInteger("State", 2);
                        NPCAllGun.SetActive(true);
                        runningGun.SetActive(false);
                        FireGun();
                    }
                }
                else
                {
                    agent.enabled = true;
                    animator.SetInteger("State", 1);
                    NPCAllGun.SetActive(false);
                    runningGun.SetActive(true);
                    agent.SetDestination(player.transform.position);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckIfPlayerVis();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (attack == true)
            {
                StartCoroutine(StopAttack());
            }
        }
    }

    private void CheckIfPlayerVis()
    {
        Vector3 rayOrigin = head.transform.position;
        Vector3 directionToEnemy = player.transform.position - transform.position;

        transform.rotation = Quaternion.LookRotation(directionToEnemy, Vector3.up);

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, head.transform.forward, out hit, Mathf.Infinity, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (attack == false)
                {
                    checking = true;
                    //say somthing (i think i saw somthing)
                    lines.text = "Someone suspects, Hide quickly!";
                    lines.gameObject.SetActive(true);

                    agent.enabled = false;
                    animator.SetInteger("State", 0);
                    NPCAllGun.SetActive(false);
                    runningGun.SetActive(false);

                    head.GetComponent<PlayEnemySounds>().PlaySound();

                    StartCoroutine(Txt());
                    StartCoroutine(Detection());

                }
                else
                {
                    attack = true;
                    counter++;
                }
            }
        }
        else
        {
            if (attack == true)
            {
                StartCoroutine(StopAttack());
            }
        }
    }


    private void SecondCheckIfPlayerVis()
    {
        Vector3 rayOrigin = head.transform.position;
        Vector3 directionToEnemy = player.transform.position - transform.position;

        transform.rotation = Quaternion.LookRotation(directionToEnemy, Vector3.up);

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, head.transform.forward, out hit, Mathf.Infinity, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (attack == false)
                {
                    //say somthing (stop right there)
                    lines.text = "Run, dont let them hit you";
                    lines.gameObject.SetActive(true);
                    dontMove.GetComponent<PlayEnemySounds>().PlaySound();
                    StartCoroutine(Txt());
                }
                attack = true;
                counter++;
            }
            else
            {
                NPCAllGun.SetActive(false);
                agent.enabled = true;
                runningGun.SetActive(true);
                animator.SetInteger("State", 1);
                agent.SetDestination(lastTarget.transform.position);
            }
        }
        checking = false;
    }


    IEnumerator Detection()
    {
        yield return new WaitForSeconds(2f);
        SecondCheckIfPlayerVis();
    }

    IEnumerator StopAttack()
    {
        int stoper = counter;
        yield return new WaitForSeconds(4f);

        if (stoper == counter)
        {
            attack = false;
            NPCAllGun.SetActive(false);
            agent.enabled = true;
            runningGun.SetActive(true);
            animator.SetInteger("State", 1);
            agent.SetDestination(lastTarget.transform.position);
        }
    }

    IEnumerator Txt()
    {
        yield return new WaitForSeconds(1f);
        lines.gameObject.SetActive(false);
    }

    private void FireGun()
    {
        Vector3 directionToEnemy = player.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        if (Time.time - lastShootTime >= shootInterval)
        {
            // Fire the gun and create the projectile (bullet) if available
            if (projectilePrefab != null && shootPoint != null)
            {
                GameObject bullet = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Add velocity to the bullet in the direction the shootPoint is facing
                    rb.velocity = shootPoint.forward * projectileSpeed;
                }
                NPCGun.SendMessage("FireAction", SendMessageOptions.DontRequireReceiver);
                gunSound.PlayDelayed(0f);
            }

            lastShootTime = Time.time;
        }
    }
}
