using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverAnimationController : MonoBehaviour
{
    public GameObject Player;
   
    [SerializeField]
    private bool _speed = false;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            _speed = true;
        else _speed = false;


        UpdateAnimations();
    }


    private void UpdateAnimations()
    {
        _animator.SetBool("speed", _speed);
        
    }

}
