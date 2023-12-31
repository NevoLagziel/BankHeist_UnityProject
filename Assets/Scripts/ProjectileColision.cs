using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileColision : MonoBehaviour
{
    private Rigidbody rb;
    public int dmg = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "player")
        {
            collision.gameObject.GetComponent<PlayerHp>().Hit(dmg);
        }else if(collision.gameObject.CompareTag("TeamMate"))
        {
            collision.gameObject.GetComponent<TeamMateHP>().Hit(dmg);
        }

        rb.velocity = Vector3.zero;
        StartCoroutine(Do());
        
    }


    IEnumerator Do()
    {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
