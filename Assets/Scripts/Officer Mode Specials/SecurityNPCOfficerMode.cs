using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SecurityNPCOfficerMode : MonoBehaviour
{
    Animator animator;
    private NavMeshAgent agent;
    
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;

    GameObject toAttack;

    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootInterval = 1f;
    public float projectileSpeed = 20f;
    private float lastShootTime;
    private float rotationSpeed = 5f;
    public GameObject NPCGun;
    public GameObject NPCAllGun;
    public bool alive;
    AudioSource gunSound;

    float distance1;
    float distance2;
    float distance3;
    float distance4;
    float distance5;

    public GameObject head;
    public bool cantShoot = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        alive = true;
        gunSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (alive == true)
        {
            if (enemy1.activeSelf == true)
            {
                distance1 = Vector3.Distance(transform.position, enemy1.transform.position);
            }
            else
            {
                distance1 = 99999f;
            }
            if (enemy2.activeSelf == true)
            {
                distance2 = Vector3.Distance(transform.position, enemy2.transform.position);
            }
            else
            {
                distance2 = 99999f;
            }
            if (enemy3.activeSelf == true)
            {
                distance3 = Vector3.Distance(transform.position, enemy3.transform.position);
            }
            else
            {
                distance3 = 99999f;
            }
            if (enemy4.activeSelf == true)
            {
                distance4 = Vector3.Distance(transform.position, enemy4.transform.position);
            }
            else
            {
                distance4 = 99999f;
            }
            if (enemy5.activeSelf == true)
            {
                distance5 = Vector3.Distance(transform.position, enemy5.transform.position);
            }
            else
            {
                distance5 = 99999f;
            }


            if (distance1 < distance2 && distance1 < distance3 && distance1 < distance4 && distance1 < distance5)
            {
                // attack enemy1
                toAttack = enemy1;
            }
            else if (distance2 < distance1 && distance2 < distance3 && distance2 < distance4 && distance2 < distance5)
            {
                // attack enemy2
                toAttack = enemy2;
            }
            else if (distance3 < distance1 && distance3 < distance2 && distance3 < distance4 && distance3 < distance5)
            {
                // attack enemy3
                toAttack = enemy3;
            }
            else if (distance4 < distance1 && distance4 < distance3 && distance4 < distance2 && distance4 < distance5)
            {
                // attack enemy4
                toAttack = enemy4;
            }
            else if (distance5 < distance1 && distance5 < distance2 && distance5 < distance3 && distance5 < distance4)
            {
                // attack enemy5
                toAttack = enemy5;
            }


            float attackDistance = Vector3.Distance(transform.position, toAttack.transform.position);
            float hightDiff = Mathf.Abs(transform.position.y - toAttack.transform.position.y);

            if (attackDistance > 45 || hightDiff > 3)
            {
                agent.enabled = true;
                animator.SetInteger("State", 1);
                NPCAllGun.SetActive(false);
                agent.SetDestination(toAttack.transform.position);
            }
            else
            {
                if (cantShoot == false)
                {

                    Vector3 rayOrigin = head.transform.position;
                    Vector3 directionToEnemy = toAttack.transform.position - rayOrigin;

                    RaycastHit hit;
                    if (Physics.Raycast(rayOrigin, directionToEnemy, out hit, directionToEnemy.magnitude, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                    {
                        if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("door") || hit.collider.CompareTag("NPC") || hit.collider.CompareTag("Enemy"))
                        {
                            agent.enabled = true;
                            animator.SetInteger("State", 1);
                            NPCAllGun.SetActive(false);
                            agent.SetDestination(toAttack.transform.position);
                        }
                        else
                        {
                            agent.enabled = false;
                            animator.SetInteger("State", 2);
                            NPCAllGun.SetActive(true);
                            FireGun();
                        }
                    }
                    else
                    {
                        agent.enabled = false;
                        animator.SetInteger("State", 2);
                        NPCAllGun.SetActive(true);
                        FireGun();
                    }
                }
                else
                {
                    agent.enabled = true;
                    animator.SetInteger("State", 1);
                    NPCAllGun.SetActive(false);
                    agent.SetDestination(toAttack.transform.position);
                }
            }
        }
        else
        {
            NPCAllGun.SetActive(false);
        }
    }

    private void FireGun()
    {
        Vector3 directionToPlayer = toAttack.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
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
