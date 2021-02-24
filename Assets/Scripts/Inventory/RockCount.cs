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

    public DialogueTrigger dialogueTrigger, dialogueFinish;

    public GameObject Box;
    public GameObject Box1;
    public GameObject Box2;

    public GameObject Palla;

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

    void OnTriggerStay(Collider rock)
    {
        if (rock!= null && rock.gameObject.CompareTag("Rock"))
        {
            opening.SetBool(Open, true);
            StartCoroutine("WaitForSec");
            

            if (Input.GetKey(KeyCode.E))
            {
                rock.gameObject.SetActive(false);

                Debug.Log(Palla.transform.position.x);
                Debug.Log(Palla.transform.position.z);
                Debug.Log(Palla.transform.position.y);

                Vector3 temp = new Vector3(Palla.transform.position.x, Palla.transform.position.y, Palla.transform.position.z);

                inseriti[rockCount].SetActive(true);
                inseriti[rockCount].transform.position = temp;
                rockCount++;
                if (rockCount == 1) 
                {
                    dialogueTrigger.SetDialog(dialogue);
                    dialogueTrigger.TriggerDialogue();
                    // FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }
            }
        }

        if (rock!= null && rock.gameObject.CompareTag("Palla"))
        {
           
            opening.SetBool(Open, true);

            if (Input.GetKey(KeyCode.E))
            {

                Vector3 temp = new Vector3(Palla.transform.position.x, Palla.transform.position.y, Palla.transform.position.z);
                rock.gameObject.SetActive(false);
                inseriti[5].SetActive(true);
                inseriti[5].transform.position = temp;
                ballCount++;
                dialogueTrigger.SetDialog(dialogueBall);
                dialogueTrigger.TriggerDialogue();
            }
        }

        if(rockCount >= 3 && ballCount >= 1)
        {
            var dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.endScene = true;
            dialogueFinish.TriggerDialogue();
            ballCount++;
        }

    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        Box.SetActive(false);
        Box1.SetActive(false);
        Box2.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Eccomi qua");
            Debug.Log(collision.name);
        }
    }
}