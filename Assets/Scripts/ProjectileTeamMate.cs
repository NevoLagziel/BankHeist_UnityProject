using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTeamMate : MonoBehaviour
{
    public int dmg = 35;
    Rigidbody rb;

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
            collision.gameObject.GetComponent<NpcHP>().Hit(dmg);

        }
        else if (collision.gameObject.CompareTag("NPC"))
        {
            collision.gameObject.GetComponent<CustomerNPC>().alive = false;
        }

        Destroy(gameObject);
    }

    IEnumerator Do()
    {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.SetActive(false);
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
