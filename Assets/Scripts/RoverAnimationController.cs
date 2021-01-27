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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            _speed = true;
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            _speed = false;

        UpdateAnimations();
    }


    private void UpdateAnimations()
    {
        _animator.SetBool("speed", _speed);
        
    }

}
