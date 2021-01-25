using GeneralUI;
using UnityEngine;

namespace Golf
{
    public class LunarModuleCollision : MonoBehaviour
    {
        [SerializeField] private DialogueTrigger dialogueTrigger;

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.CompareTag("Palla"))
                dialogueTrigger.TriggerDialogue();
        }
    }
}