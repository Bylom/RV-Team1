using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using GeneralUI;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject Area_Flag;

    public Vector3 PickPosition;
    public Vector3 PickRotation;


    [SerializeField] private DialogueTrigger dialogueTrigger;
    private bool _firstDialogueCall = true;

    public void Start()
    {
        Band = GetComponent<Rigidbody>();
        StartCoroutine("WaitForSec");
    }

    public void Update()
    {
        if (NearFlag)
        {
            if (Input.GetKey(KeyCode.E))
            {                
                coll.isTrigger = false;
                Flag.transform.parent = hand.transform;
                Flag.transform.localPosition = PickPosition;
                Flag.transform.localEulerAngles = PickRotation;
                Area_Flag.SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_firstDialogueCall)
        {
            _firstDialogueCall = false;
            dialogueTrigger.TriggerDialogue();
        }
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player");
            //FindObjectOfType<AudioManager>().Play("Plant_Flag");
            //press.text = "Press E to interact";
            //press.gameObject.SetActive(true);
            NearFlag = true;
        }

        if (other.gameObject.tag == "Flag")
        {
            FindObjectOfType<AudioManager>().Play("Plant_Flag");
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            NearFlag = false;
            //press.gameObject.SetActive(false);
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        FindObjectOfType<AudioManager>().Play("Take_Flag");
    }
}
