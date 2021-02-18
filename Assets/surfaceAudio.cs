using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class surfaceAudio : MonoBehaviour
{
    private bool primo;
    // Start is called before the first frame update
    void Start()
    {
            

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player") && !primo)
        {
            FindObjectOfType<AudioManager>().Play("Surface");
            primo = true;
        }
        
    }
}
