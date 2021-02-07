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
    public Rigidbody bandiera;
    public GameObject Event_flag;

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
                //putFlag = true;
                //bandiera.GetComponent<Bandiera>().Band.isKinematic = false;
                isFlag = false;
                testo.gameObject.SetActive(false);
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

    public virtual void OnPlantFlag()
    {
        Debug.Log("La bandiera è stata piantata");
        //bandiera.GetComponent<Bandiera>().Band.isKinematic = false;
        Event_flag.GetComponent<Bandiera>().Flag.transform.parent = null;
        Event_flag.GetComponent<Bandiera>().Flag.transform.localRotation = Quaternion.Euler(Vector3.zero);
        StartCoroutine("WaitForSec");
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("1 secondo");
        bandiera.GetComponent<Bandiera>().Band.isKinematic = true;
    }
}
