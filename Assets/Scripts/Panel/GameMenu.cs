using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    bool paused;
    float pause;
    public GameObject Inst;

    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void ResumeGame()
    {
        if (Time.timeScale == 0) { Time.timeScale = pause; }
        paused = false;
    }

    public void Instruction()
    {
        Inst.SetActive(true);
    }

    public void Returen()
    {
        Inst.SetActive(false);
    }
}