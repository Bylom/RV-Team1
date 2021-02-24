using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;
using UnityEngine.UI;
using GeneralUI;
using UnityEngine.SceneManagement;

public class End_First_Scene : MonoBehaviour
{
    public bool active_A = false;
    private bool m_Step;

    public Dialogue dialogue;

    public DialogueTrigger dialogueTrigger;

    public Image BlackIm;

    // Update is called once per frame
    void Update()
    {
        if (active_A == true)
        {
            StartCoroutine(MakeFirstStep());
        }
    }

    IEnumerator MakeFirstStep()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("End Scene");
        //m_Step = true;
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.endScene = true;
        dialogueTrigger.SetDialog(new Dialogue()
        {
            name = "Mission control",
            sentences = new[]
            {
                 "Congrats! You just made the first step on the Moon"
            }
        });

        dialogueTrigger.TriggerDialogue();
        //SceneManager.LoadScene("Scenes/Missioni");
        Cursor.lockState = CursorLockMode.None;

        yield return new WaitForSeconds(10);
        BlackIm.CrossFadeAlpha(1, 2, false);

    }
}
