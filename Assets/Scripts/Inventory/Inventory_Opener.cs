using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory_Opener : MonoBehaviour
{

    Animator animator;
    public Text testo;

    public bool NearInvent = false;
    public bool open = false;
    public bool closed = true;

    public GameObject Camera;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (NearInvent)
        {
            if (closed)
            {
                if (Input.GetKey(KeyCode.I))
                {
                    animator.SetBool("Open", true);
                    StartCoroutine("WaitForSec");
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    //Camera.GetComponent<Camera_Movement>().enabled = true;
                    //Camera.GetComponent<Animator>().enabled = true;
                }
            }
            if (open)
            {
                
                if (Input.GetKey(KeyCode.I))
                {
                    animator.SetBool("Open", false);
                    StartCoroutine("WaitForSec2");
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            testo.text = "Press I to interact";
            testo.gameObject.SetActive(true);
            NearInvent = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            testo.gameObject.SetActive(false);
            NearInvent = false;
        }

    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        open = true;
        closed = false;
    }

    IEnumerator WaitForSec2()
    {
        yield return new WaitForSeconds(3);
        open = false;
        closed = true;
    }
}
