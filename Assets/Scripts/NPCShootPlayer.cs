using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShootPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject projectilePrefab;
    public float shootInterval = 1f;
    public float projectileSpeed = 10f;

    private float lastShootTime;

    private void Start()
    { 

    }

    private void Update()
    {
        if (player == null) return;

        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Check if it's time to shoot
        if (Time.time - lastShootTime >= shootInterval)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            rb.velocity = directionToPlayer.normalized * projectileSpeed;
        }
    }
}
