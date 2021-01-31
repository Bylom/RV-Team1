using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{

    public Text testo;
    Animator animator;
    public bool isFlag = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void DropCurrentItem()
    {
        animator.SetBool("Flag", true);
    }

    private void Update()
    {
        /*
        if(isFlag == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetBool("Flag", true);
            }
        }
        */
        if (Input.GetKey(KeyCode.E))
        {
            DropCurrentItem();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Flag"))
        {
            Debug.Log("Puoi piantare la bandiera");
            testo.text = "Press E per piantare la bandiera";
            testo.gameObject.SetActive(true);
            isFlag = true;
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Flag"))
        {
            Debug.Log("Ciao Cristian");
            testo.gameObject.SetActive(true);
        }
    }
}
