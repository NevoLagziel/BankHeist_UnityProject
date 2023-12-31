using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerPlayerProjectile : MonoBehaviour
{
    private Rigidbody rb;
    public int dmg = 20;
    public GameObject hitSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);

        if (collision.gameObject.CompareTag("TeamMate"))
        {
            Instantiate(hitSound, transform.position, transform.rotation);
            collision.gameObject.GetComponent<RobberNPCHP>().Hit(dmg);
        }

        
        Destroy(gameObject);

    }
}
