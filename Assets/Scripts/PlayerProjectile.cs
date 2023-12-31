using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public int dmg = 50;
    Rigidbody rb;
    public GameObject hitSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("RoomEnemy"))
        {
            Instantiate(hitSound, transform.position, transform.rotation);
            collision.gameObject.GetComponent<NpcHP>().Hit(dmg);

        }else if (collision.gameObject.CompareTag("NPC"))
        {
            Instantiate(hitSound, transform.position, transform.rotation);
            collision.gameObject.GetComponent<CustomerNPC>().alive = false;
        }

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
