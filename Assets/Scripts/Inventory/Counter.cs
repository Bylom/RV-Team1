using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{

    public GameObject counter_1;
    public GameObject counter_2;
    public Text testo;
    public bool go = false;


    // Update is called once per frame
    void Update()
    {
        if(counter_1.GetComponent<ItemClickHandler>().counter == 1 && counter_2.GetComponent<ItemClickHandler>().counter == 1)
        {
            go = true;
            testo.text = "You can press E to exit from the inventory";
        }

        if (counter_1.GetComponent<ItemClickHandler>().counter == 0 && counter_2.GetComponent<ItemClickHandler>().counter == 1 || 
            counter_1.GetComponent<ItemClickHandler>().counter == 1 && counter_2.GetComponent<ItemClickHandler>().counter == 0 || 
            counter_1.GetComponent<ItemClickHandler>().counter == 0 && counter_2.GetComponent<ItemClickHandler>().counter == 0)
        {
            if(Input.GetKeyDown(KeyCode.E))
            testo.text = "You must select all objects";
            testo.gameObject.SetActive(true);
        }
    }
}
