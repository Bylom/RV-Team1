using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralUI;

public class BorderCollision : MonoBehaviour
    {
        [SerializeField] private DialogueTrigger dialogueTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Rover"))
            {
                dialogueTrigger.TriggerDialogue();
                FindObjectOfType<AudioManager>().Play("Where");
            }
        }
    }

