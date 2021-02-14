using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralUI;

public class BorderCollision : MonoBehaviour
    {
        [SerializeField] private DialogueTrigger dialogueTrigger;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Rover"))
            {
                dialogueTrigger.TriggerDialogue();
            }
        }
    }

