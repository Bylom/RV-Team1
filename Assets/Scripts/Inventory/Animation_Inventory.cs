using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Inventory : MonoBehaviour
{
    Animator animator;
    public GameObject script_opener;
    public GameObject camera;
    public GameObject head;
    


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(script_opener.GetComponent<Inventory_Opener>().closed == true)
        {
            if (Input.GetKey(KeyCode.I))
            {
                //camera.transform.parent = head.transform;
                
                animator.SetBool("Inventory", true);
            }
        }
        if (script_opener.GetComponent<Inventory_Opener>().open == true)
        {
            if (Input.GetKey(KeyCode.I))
            {
                animator.SetBool("Inventory", false);
            }
        }
    }


}
