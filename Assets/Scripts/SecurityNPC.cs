using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class SecurityNPC : MonoBehaviour
{
    //public GameObject player;
    /*Animator animator;
    public GameObject target1;
    public GameObject target2;
    private NavMeshAgent agent;
    public GameObject player;
    public GameObject gun;
    bool attack;*/

    Animator animator;
    //public Text objTxt;
    public TMP_Text objTxt;
    public GameObject target1;
    public GameObject target2;
    private NavMeshAgent agent;
    public GameObject player;
    public GameObject gun;
    public GameObject projectilePrefab;
    public bool attack = false;
    public Transform shootPoint;
    public float shootInterval = 1f;
    public float projectileSpeed = 20f;
    private float lastShootTime;
    private float rotationSpeed = 5f;
    public GameObject NPCGun;
    public GameObject NPCAllGun;
    public bool alive;
    public GameObject key;
    AudioSource gunSound;
    public GameObject teamMate;
    bool attackMate;
    public GameObject alarm;

    public GameObject head;
    public bool cantShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if(target2.tag == "Target")
            agent.SetDestination(target2.transform.position);
        alive = true;      
        gunSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (alive == true)
        {
            float distance1 = Vector3.Distance(transform.position, target1.transform.position);
            float distance2 = Vector3.Distance(transform.position, target2.transform.position);
            float playerDistance = Vector3.Distance(transform.position, player.transform.position);


            if (attack == false)
            {
                if (target1.tag == "Target" && target2.tag == "Target")
                {
                    animator.SetInteger("State", 1);
                    if (distance1 < 2)
                    {
                        agent.SetDestination(target2.transform.position);
                    }
                    if (distance2 < 2)
                    {
                        agent.SetDestination(target1.transform.position);
                    }
                }
            }
            else
            {
                if (teamMate.activeSelf == true)
                {
                    float mateDistance = Vector3.Distance(transform.position, teamMate.transform.position);
                    if (playerDistance > mateDistance)
                        attackMate = true;
                    else
                        attackMate = false;
                }
                if (attackMate == false)
                {
                    float hightDiff = Mathf.Abs(transform.position.y - player.transform.position.y);

                    if (playerDistance > 45 || hightDiff > 3)
                    {
                        agent.enabled = true;
                        animator.SetInteger("State", 1);
                        NPCAllGun.SetActive(false);
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

                                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("door") || hit.collider.CompareTag("Enemy"))
                                {
                                    agent.enabled = true;
                                    animator.SetInteger("State", 1);
                                    NPCAllGun.SetActive(false);
                                    agent.SetDestination(player.transform.position);
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
                            agent.SetDestination(player.transform.position);
                        }
                    }
                }
                else
                {
                    if (teamMate.activeSelf == true)
                    {
                        float mateDistance = Vector3.Distance(transform.position, teamMate.transform.position);
                        float hightDiff = Mathf.Abs(transform.position.y - teamMate.transform.position.y);

                        if (mateDistance > 45 || hightDiff > 3)
                        {
                            agent.enabled = true;
                            animator.SetInteger("State", 1);
                            NPCAllGun.SetActive(false);
                            agent.SetDestination(teamMate.transform.position);
                        }
                        else
                        {
                            if (cantShoot == false)
                            {
                                Vector3 rayOrigin = head.transform.position;
                                Vector3 directionToEnemy = teamMate.transform.position - rayOrigin;
                                RaycastHit hit;

                                if (Physics.Raycast(rayOrigin, directionToEnemy, out hit, directionToEnemy.magnitude, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                                {

                                    if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("door") || hit.collider.CompareTag("Enemy"))
                                    {
                                        agent.enabled = true;
                                        animator.SetInteger("State", 1);
                                        NPCAllGun.SetActive(false);
                                        agent.SetDestination(teamMate.transform.position);
                                    }
                                    else
                                    {
                                        agent.enabled = false;
                                        animator.SetInteger("State", 2);
                                        NPCAllGun.SetActive(true);
                                        FireGunMate();
                                    }
                                }
                                else
                                {
                                    agent.enabled = false;
                                    animator.SetInteger("State", 2);
                                    NPCAllGun.SetActive(true);
                                    FireGunMate();
                                }
                            }
                            else
                            {
                                agent.enabled = true;
                                animator.SetInteger("State", 1);
                                NPCAllGun.SetActive(false);
                                agent.SetDestination(teamMate.transform.position);
                            }
                        }
                    }
                    else
                    {
                        attackMate = false;
                    }
                }
            }

        }
        else
        {
            NPCAllGun.SetActive(false);

            if (key.tag == "Key")
            {
                key.transform.position = transform.position;
                key.SetActive(true);
                objTxt.text = "Collect the key for the money room.";
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (attack == false)
        {
            if (other.name == "player")
            {
                if (gun.gameObject.activeSelf)
                {
                    attack = true;
                    alarm.GetComponent<AlarmSoundScript>().PlaySound();
                    attackMate = false;
                }
            }
            else if (other.CompareTag("TeamMate") && other.isTrigger == false)
            {
                attack = true;
                attackMate = true;
                alarm.GetComponent<AlarmSoundScript>().PlaySound();
            }
        }
    }


    private void FireGun()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
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

    private void FireGunMate()
    {
        Vector3 directionToMate = teamMate.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToMate, Vector3.up);
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
