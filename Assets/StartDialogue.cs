using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using GeneralUI;

public class StartDialogue : MonoBehaviour
{
    [SerializeField] private DialogueTrigger dialogueTrigger;
    private bool _firstDialogueCall = true;

    private void FixedUpdate()
    {
        if (_firstDialogueCall)
        {
            _firstDialogueCall = false;
            dialogueTrigger.TriggerDialogue();
        }
    }
}
