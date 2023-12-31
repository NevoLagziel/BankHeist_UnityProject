using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchCameras : MonoBehaviour
{
    public GameObject camera_1;
    public GameObject camera_2;
    public GameObject teamMate;
    AudioSource carSound;

    // Start is called before the first frame update
    void Start()
    {
        carSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCamera()
    {
        camera_2.SetActive(true);
        camera_1.SetActive(false);
        teamMate.SetActive(false);
        GameObject.FindGameObjectWithTag("Screen").SetActive(false);
        camera_2.gameObject.GetComponent<Animator>().SetBool("State",true);
        GameObject.FindGameObjectWithTag("Car").GetComponent<Animator>().SetBool("State", true);
        carSound.PlayDelayed(0f);
        StartCoroutine(Do());
    }

    IEnumerator Do()
    {
        yield return new WaitForSeconds(3.5f);
        carSound.Stop();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("VictoryScene");
    }
}
