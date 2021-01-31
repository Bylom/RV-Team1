using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Bandiera : MonoBehaviour
{
    // Start is called before the first frame update

    [FormerlySerializedAs("Hand")] public GameObject hand;

    public GameObject Flag;
    public Rigidbody Band;
    public float dropForwardForce, dropUpwardForce;
    public Transform player;
    public BoxCollider coll;

    public void Start()
    {
        Band = GetComponent<Rigidbody>();
    }

    public void Update()
    {


        if (Input.GetKey(KeyCode.Z))
        {
            //Band.isKinematic = false;
            coll.isTrigger = false;
            Debug.Log("Eccomi qua");
            Flag.transform.localRotation = Quaternion.Euler(Vector3.zero);
            //Flag.transform.localPosition = Vector3.zero;
            Flag.transform.parent = null;
            //Band.AddForce(transform.forward * 2.0f, ForceMode.Impulse);
            //Band.AddForce(player.forward * dropForwardForce, ForceMode.Impulse);
            //Band.AddForce(player.up * dropUpwardForce, ForceMode.Impulse);

        }
    }
    
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Flag.transform.parent = hand.transform;
            Flag.transform.localPosition = hand.transform.localPosition;
            Flag.transform.localRotation = hand.transform.localRotation;

        }
    }
    
}
