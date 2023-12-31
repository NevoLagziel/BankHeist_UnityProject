using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class TeamMateNPC : MonoBehaviour
{

    Animator animator;
    public TMP_Text followTxt;
    public TMP_Text forceFollowTxt;
    private NavMeshAgent agent;
    public GameObject player;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    private GameObject enemy;
    public bool attack;
    public bool alive;
    public bool followAndAttack;
    public bool followNow;
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

    //public GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        gunSound = GetComponent<AudioSource>();
        followAndAttack = false;
        followNow = false;
        alive = true;
        attack = false;
        enemy = null;
    }

    void Update()
    {
        if (alive == true)
        {
            float playerDistance = Vector3.Distance(transform.position, player.transform.position);
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                followAndAttack = !followAndAttack;
                if (followAndAttack == true)
                    followTxt.text = "Press Q to make teammate stop following";
                else
                    followTxt.text = "Press Q to make teammate follow and attack";
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                attack = false;
                followNow = !followNow;
                followAndAttack = true;

                if (followNow == true)
                {
                    forceFollowTxt.text = "Press M to stop teammate force follow";
                    followTxt.enabled = false;
                }
                else
                {
                    forceFollowTxt.text = "Press M to force teammate to follow";
                    followTxt.enabled = true;
                    followTxt.text = "Press Q to make teammate stop following";
                }
            }

            if (followNow == true)
            {
                NPCAllGun.SetActive(false);

                if (playerDistance <= 15)
                {
                    agent.enabled = false;
                    runningGun.SetActive(false);
                    animator.SetInteger("State", 0);
                }
                if (playerDistance > 15)
                {
                    agent.enabled = true;
                    runningGun.SetActive(true);
                    animator.SetInteger("State", 1);
                    agent.SetDestination(player.transform.position);
                }
            }
            else
            {
                if (attack == false)
                {
                    NPCAllGun.SetActive(false);

                    if (followAndAttack == true)
                    {
                        if (playerDistance <= 15)
                        {
                            agent.enabled = false;
                            runningGun.SetActive(false);
                            animator.SetInteger("State", 0);
                        }
                        if (playerDistance > 15)
                        {
                            agent.enabled = true;
                            runningGun.SetActive(true);
                            animator.SetInteger("State", 1);
                            agent.SetDestination(player.transform.position);
                        }
                    }
                    else
                    {
                        agent.enabled = false;
                        runningGun.SetActive(false);
                        animator.SetInteger("State", 0);
                    }
                }
                else
                {
                    if (enemy != null)
                    {
                        if (enemy.activeSelf == true)
                        {
                            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
                            float hightDiff = Mathf.Abs(transform.position.y - enemy.transform.position.y);

                            if (enemyDistance > 40 || hightDiff > 3)
                            {
                                agent.enabled = true;
                                animator.SetInteger("State", 1);
                                NPCAllGun.SetActive(false);
                                runningGun.SetActive(true);
                                agent.SetDestination(enemy.transform.position);
                            }
                            else
                            {
                                Vector3 rayOrigin = head.transform.position;
                                Vector3 directionToEnemy = enemy.transform.position - rayOrigin;

                                RaycastHit hit;

                                if (cantShoot == false)
                                {
                                    if (Physics.Raycast(rayOrigin, directionToEnemy, out hit, directionToEnemy.magnitude,Physics.AllLayers,QueryTriggerInteraction.Ignore))
                                    {
                                        if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("door"))
                                        {
                                            agent.enabled = true;
                                            animator.SetInteger("State", 1);
                                            NPCAllGun.SetActive(false);
                                            runningGun.SetActive(true);
                                            agent.SetDestination(enemy.transform.position);

                                        }/*else if (hit.collider.CompareTag("DontShoot"))
                                        {
                                            if(hit.collider.GetComponent<ShootlessZoneScript>().shootT == true)
                                            {
                                                agent.enabled = false;
                                                animator.SetInteger("State", 2);
                                                NPCAllGun.SetActive(true);
                                                runningGun.SetActive(false);
                                                FireGun();
                                            }
                                            else
                                            {
                                                agent.enabled = true;
                                                animator.SetInteger("State", 1);
                                                NPCAllGun.SetActive(false);
                                                runningGun.SetActive(true);
                                                agent.SetDestination(enemy.transform.position);
                                            }
                                        }*/
                                        /*else if (hit.collider.CompareTag("door"))
                                        {
                                            agent.enabled = true;
                                            animator.SetInteger("State", 1);
                                            NPCAllGun.SetActive(false);
                                            runningGun.SetActive(true);
                                            agent.SetDestination(enemy.transform.position);
                                        }*/
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

                                /*RaycastHit hit;
                                if (Physics.Raycast(head.transform.position, directionToEnemy, out hit, directionToEnemy.magnitude))
                                {
                                    if (hit.collider.CompareTag("door"))
                                    {
                                        agent.enabled = true;
                                        animator.SetInteger("State", 1);
                                        NPCAllGun.SetActive(false);
                                        runningGun.SetActive(true);
                                        agent.SetDestination(enemy.transform.position);
                                    }
                                    else if (hit.collider.CompareTag("Wall"))
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
                                }*/
                            }
                        }
                        else
                        {
                            attack = false;
                        }
                    }
                }
            }
        }
        else
        {
            NPCAllGun.SetActive(false);
            runningGun.SetActive(false);
            followTxt.enabled = false;
            forceFollowTxt.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (attack == false && followNow == false)
        {
            if (other.isTrigger == false)
            {
                if (other.CompareTag("Enemy") || other.CompareTag("RoomEnemy"))
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
