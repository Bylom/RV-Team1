using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_Step : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator m_Animator;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            m_Animator.SetBool("Ladder", true);
        }
    }
}
