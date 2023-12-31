using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RobberNPCScript : MonoBehaviour
{
    Animator animator;
    //public TMP_Text robbersLeft;
    //public TMP_Text moneyStolen;
    private NavMeshAgent agent;
    public GameObject escape;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    private GameObject enemy;
    public bool attack;
    public bool alive;
    

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
    public Text moneyLost;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        gunSound = GetComponent<AudioSource>();
        //followAndAttack = false;
        //followNow = false;
        alive = true;
        attack = false;
        enemy = null;
        Money.MoneyCounter += 20;
        moneyLost.text = "- "+(Money.MoneyCounter * 1000);
    }

    void Update()
    {
        if (alive == true)
        {
            if (attack == false)
            {
                NPCAllGun.SetActive(false);
                agent.enabled = true;
                runningGun.SetActive(true);
                animator.SetInteger("State", 1);
                agent.SetDestination(escape.transform.position);
            }
            else
            {
                if (enemy != null)
                {
                    if (enemy.activeSelf == true)
                    {
                        float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
                        float hightDiff = Mathf.Abs(transform.position.y - enemy.transform.position.y);

                        if (enemyDistance >= 70)
                        {
                            attack = false;
                        }
                        else if ((enemyDistance > 40 && enemyDistance < 70) || hightDiff > 3)
                        {
                            agent.enabled = true;
                            animator.SetInteger("State", 1);
                            NPCAllGun.SetActive(false);
                            runningGun.SetActive(true);
                            agent.SetDestination(enemy.transform.position);
                        }
                        else
                        {
                            if (cantShoot == false)
                            {

                                Vector3 rayOrigin = head.transform.position;
                                Vector3 directionToEnemy = enemy.transform.position - rayOrigin;

                                RaycastHit hit;
                                if (Physics.Raycast(rayOrigin, directionToEnemy, out hit, directionToEnemy.magnitude, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                                {
                                    if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("door") || hit.collider.CompareTag("TeamMate"))
                                    {
                                        agent.enabled = true;
                                        animator.SetInteger("State", 1);
                                        NPCAllGun.SetActive(false);
                                        runningGun.SetActive(true);
                                        agent.SetDestination(enemy.transform.position);
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
                                agent.SetDestination(enemy.transform.position);
                            }
                        }
                    }
                    else
                    {
                        attack = false;
                    }
                }
            }
        }
        else
        {
            NPCAllGun.SetActive(false);
            runningGun.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (attack == false)
        {
            if (other.isTrigger == false)
            {
                if (other.CompareTag("Enemy") || other.CompareTag("RoomEnemy") || other.CompareTag("Player"))
                {
                    enemy = other.gameObject;
                    attack = true;
                }
            }
        }
    }


    private void FireGun()
    {
        Vector3 directionToEnemy = enemy.transform.position - transform.position;
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
