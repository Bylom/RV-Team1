using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class take : MonoBehaviour
{

    private Animator m_Animator;
    public bool astronaut;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Sasso"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                astronaut = true;
            }
        }
    }        
}
