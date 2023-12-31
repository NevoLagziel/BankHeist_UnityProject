using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.Animations;

public class PlayerWalking : MonoBehaviour
{
    CharacterController cController;
    float speed = 0, sideSpeed = 0;
    float angularSpeed = 120f;
    public GameObject aCamera; // must be defined in Unity
    private AudioSource footStep;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // attaching CharacterController of player to cController
        cController = GetComponent<CharacterController>();
        footStep = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // example of simplest motion
        // transform.Translate(new Vector3(0, 0, 0.1f));

        float rotationAboutY, rotationAboutX;

        // Time.deltaTime is a time between adjacent frames 
        rotationAboutY = (angularSpeed * Input.GetAxis("Mouse X") * Time.deltaTime);
        rotationAboutX = (-angularSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime);

        // acts on Player (this)
        transform.Rotate(new Vector3(0, rotationAboutY, 0));

        aCamera.transform.Rotate(new Vector3(rotationAboutX, 0, 0));

        // getAxis returns -1 , 0 or +1
        speed = Input.GetAxis("Vertical");
        sideSpeed = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(20 * sideSpeed * Time.deltaTime, -0.5f, 20 * speed * Time.deltaTime);
        // change direction to Global coordinates
        direction = transform.TransformDirection(direction);
        cController.Move(direction); // Move gets Vector3 in Global coordinates

        if ((speed != 0 || sideSpeed != 0) && !footStep.isPlaying)
        {
            footStep.Play();
        }
        
        if((speed != 0 || sideSpeed != 0))
        {
            if(animator.GetInteger("State") == 0)
                animator.SetInteger("State", 1);
        }
        else
        {
            if (animator.GetInteger("State") != 0)
                animator.SetInteger("State", 0);
        }
    }
}
