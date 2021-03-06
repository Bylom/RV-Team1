﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    public bool GameIsPaused = false;

    public bool NearInventory = false;

    public GameObject InventoryPanelUI;
    public Text press;
    public Text error;
    // Update is called once per frame

    void Update()
    {

        press.gameObject.SetActive(false);

        if (NearInventory)
        {
            //press.text = "Press I to open Inventory";
            //press.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (GameIsPaused)
                {
                    if (InventoryPanelUI.GetComponent<Counter>().go == true)
                        Resume();
                }

                else
                {
                    Pause();
                    error.text = "Click with left mouse to pickup objects";
                    error.gameObject.SetActive(true);
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
        StartCoroutine("WaitForSec");
        
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        InventoryPanelUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
