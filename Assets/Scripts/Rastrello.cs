using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GeneralUI;
using UnityEngine.Serialization;

public class Rastrello : MonoBehaviour
{
    [FormerlySerializedAs("Hand")] public GameObject hand;
    public GameObject rastr;
    public Dialogue dialogue;
    

    public Vector3 PickPosition;
    public Vector3 PickRotation;

    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("Materials");
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void Update()
    {
                rastr.transform.parent = hand.transform;
                rastr.transform.localPosition = PickPosition;
                rastr.transform.localEulerAngles = PickRotation;
    }



}
