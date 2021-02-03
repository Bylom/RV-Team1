using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Camera_Movement : MonoBehaviour
{

    Animator animator;
    public Inventory_Opener mov;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mov.closed && mov.NearInvent)
        {
            StartCoroutine("WaitForSec");
            
        }
        if (mov.open)
        {
            if (Input.GetKey(KeyCode.I))
            {
                animator.SetBool("Down", false);
                Debug.Log("Testa Sale");
            }
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool("Down", true);
        Debug.Log("Testa Scende");
    }
}
