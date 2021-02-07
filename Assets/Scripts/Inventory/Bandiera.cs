using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class Bandiera : MonoBehaviour
{
    // Start is called before the first frame update

    [FormerlySerializedAs("Hand")] public GameObject hand;

    public GameObject Flag;
    public GameObject isF;
    public Rigidbody Band;
    public float dropForwardForce, dropUpwardForce;
    public Transform player;
    public BoxCollider coll;
    public Text press;
    public bool NearFlag = false;

    public Vector3 PickPosition;
    public Vector3 PickRotation;

    public void Start()
    {
        Band = GetComponent<Rigidbody>();
    }

    public void Update()
    {

        if (isF.GetComponent<Flag>().putFlag == true)
        {
            Debug.Log("stop");
            
            //Flag.transform.localRotation = Quaternion.Euler(Vector3.zero);
            //Flag.transform.forward = Vector3.zero;
            //Flag.transform.parent = null;
            //StartCoroutine("WaitForSec");
        }
        if (NearFlag)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Player");
                
                coll.isTrigger = false;
                Flag.transform.parent = hand.transform;
                Flag.transform.localPosition = PickPosition;
                Flag.transform.localEulerAngles = PickRotation;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player");
            press.text = "Press E to interact";
            press.gameObject.SetActive(true);
            NearFlag = true;
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            NearFlag = false;
            press.gameObject.SetActive(false);
        }
    }

}
