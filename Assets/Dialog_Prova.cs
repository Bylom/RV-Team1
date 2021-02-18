using GeneralUI;
using UnityEngine;

public class Dialog_Prova : MonoBehaviour
{
    [SerializeField] private DialogueTrigger dialogueTrigger;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueTrigger.TriggerDialogue();
        }
    }
 
}
