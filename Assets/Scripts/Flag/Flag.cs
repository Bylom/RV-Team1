using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{
    public Text testo;
    Animator animator;
    public bool isFlag = false;
    public bool putFlag = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isFlag == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetBool("Flag", true);
                putFlag = true;
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Flag")
        {
            Debug.Log("Puoi piantare la bandiera");
            testo.text = "Press E per piantare la bandiera";
            testo.gameObject.SetActive(true);
            isFlag = true;
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Flag")
        {
            Debug.Log("Ciao Cristian");
            testo.gameObject.SetActive(false);
        }
    }
}
