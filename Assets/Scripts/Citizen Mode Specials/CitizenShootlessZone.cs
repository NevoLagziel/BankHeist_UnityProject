using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenShootlessZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TeamMate") && other.isTrigger == false)
        {
            other.gameObject.GetComponent<EnemyAttackPlayer>().cantShoot = true;
        }
        else if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("RoomEnemy")) && other.isTrigger == false)
        {
            other.gameObject.GetComponent<SecurityNPCOfficerMode>().cantShoot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TeamMate") && other.isTrigger == false)
        {
            other.gameObject.GetComponent<EnemyAttackPlayer>().cantShoot = false;
        }
        else if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("RoomEnemy")) && other.isTrigger == false)
        {
            other.gameObject.GetComponent<SecurityNPCOfficerMode>().cantShoot = false;
        }
    }
}
