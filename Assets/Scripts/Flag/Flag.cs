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
    float m_ScaleX, m_ScaleY, m_ScaleZ;

    void Start()
    {
        animator = GetComponent<Animator>();
        m_ScaleX = 0.05f;
        m_ScaleY = 1.5f;
        m_ScaleZ = 0.05f;
    }

    private void Update()
    {
        if (isFlag == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetBool("Flag", true);
                isFlag = false;
                testo.gameObject.SetActive(false);
                Event_flag.GetComponent<Bandiera>().coll.size = new Vector3(m_ScaleX, m_ScaleY, m_ScaleZ);
                StartCoroutine("WaitForSec2");
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Flag")
        {
            testo.text = "Press E per piantare la bandiera";
            testo.gameObject.SetActive(true);
            isFlag = true;
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Flag")
        {
            testo.gameObject.SetActive(false);
        }
    }

    public virtual void OnPlantFlag()
    {
        Event_flag.GetComponent<Bandiera>().Flag.transform.parent = null;
        StartCoroutine("WaitForSec");
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1);
        bandiera.GetComponent<Bandiera>().Band.isKinematic = true;
    }
    IEnumerator WaitForSec2()
    {
        yield return new WaitForSeconds(10);
        //Event_flag.GetComponent<Bandiera>().coll.enabled = true;
        //Event_flag.GetComponent<Bandiera>().coll.isTrigger = false;
    }
}
