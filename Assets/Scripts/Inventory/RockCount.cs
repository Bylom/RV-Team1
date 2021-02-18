using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralUI;
using UnityEngine.SceneManagement;

public class RockCount : MonoBehaviour
{

    private Animator opening;
    public int rockCount = 0;
    public int ballCount = 0;

    private int numSassi = 6;
    public GameObject[] inseriti;
    public Dialogue dialogue;
    public Dialogue dialogueBall;
    public Dialogue dialogueFinish;

    public DialogueTrigger dialogueTrigger;

    private static readonly int Open = Animator.StringToHash("Open");



    // Start is called before the first frame update
    void Start()
    {
        inseriti = new GameObject[numSassi];
        for(int i=0; i<numSassi; i++)
        {
            inseriti[i] = GameObject.Find("sasso" + i);
            inseriti[i].SetActive(false);
        }

        opening = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider rock)
    {
        if (rock!= null && rock.gameObject.CompareTag("Rock"))
        {
            opening.SetBool("Open", true);

            if (Input.GetKey(KeyCode.E))
            {
                rock.gameObject.SetActive(false);
                inseriti[rockCount].SetActive(true);
                rockCount++;
                if (rockCount == 1) 
                {
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }
            }
        }

        if (rock!= null && rock.gameObject.CompareTag("Palla"))
        {
           
            opening.SetBool("Open", true);

            if (Input.GetKey(KeyCode.E))
            {
                rock.gameObject.SetActive(false);
                inseriti[5].SetActive(true);
                ballCount++;
                FindObjectOfType<DialogueManager>().StartDialogue(dialogueBall);
            }
        }

        if(rockCount == 3 && ballCount == 1)
        {
            var dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.endScene = true;
            dialogueManager.StartHistoricDialogue(dialogueFinish);
            ballCount++;
        }

    }

}
