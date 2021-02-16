using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
   public void PlayGame()
   {
       int val = PlayerPrefs.GetInt("levelAt", 1);
       Debug.Log("Next scene");
        SceneManager.LoadScene(val + 1);
    }
}
