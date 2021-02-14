using GeneralUI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Golf
{
    public class Rover_Collision : MonoBehaviour
    {
        
        [SerializeField] private DialogueTrigger dialogueTrigger;


        private void Start()
        {
  
 
        }

        private void OnCollisionEnter(Collision other)
        {

            if (other.gameObject.CompareTag("Border"))
            {

                
                dialogueTrigger.TriggerDialogue();
            }
            }

        }


    
}
