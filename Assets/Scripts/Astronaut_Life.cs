using System.Collections;
using System.Collections.Generic;
using GeneralUI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Astronaut_Life : MonoBehaviour
{
    public float currentTime = 600f;
    [SerializeField] private DialogueTrigger dialogueTrigger;

    // Update is called once per frame
    void Update()
    {
        

        currentTime -= 1 * Time.deltaTime;

        if(currentTime <= 200)
        {
            dialogueTrigger.TriggerDialogue();
        }

        if (currentTime < 0)
            currentTime = 0;

        if (currentTime == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
