using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessBullet : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        StartCoroutine(Do());
    }

    IEnumerator Do()
    {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
