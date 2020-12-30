using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;

    public bool NearInventory = false;

    public GameObject InventoryPanelUI;
    // Update is called once per frame
    void Update()
    {

        if (NearInventory)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (GameIsPaused)
                {
                    Resume();
                }

                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        InventoryPanelUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        InventoryPanelUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

}
