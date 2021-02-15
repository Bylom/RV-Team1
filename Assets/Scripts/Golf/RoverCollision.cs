using System;
using GeneralUI;
using UnityEngine;

namespace Golf
{
    public class RoverCollision : MonoBehaviour
    {
        [SerializeField] private DialogueTrigger dialogueTrigger;
        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.CompareTag("Palla"))
                dialogueTrigger.TriggerDialogue();
        }
    }
}
