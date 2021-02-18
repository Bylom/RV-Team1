using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GeneralUI;

public class FadingFirstStep : MonoBehaviour
{
    public Image BlackIm;
    public Dialogue dialogue;

    void Start()
    {
        StartCoroutine(Attendi());
        FindObjectOfType<AudioManager>().Play("Landed");

    }

    void FadeIn()
    {
        BlackIm.CrossFadeAlpha(1, 2, false);
    }

    IEnumerator Attendi()
    {
        yield return new WaitForSeconds(5);
        BlackIm.CrossFadeAlpha(0, 1, false);

        yield return new WaitForSeconds(3);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        FindObjectOfType<AudioManager>().Play("Proceed");
    }

}
