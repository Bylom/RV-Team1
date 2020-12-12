using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPController : MonoBehaviour {
    public GameObject Player;

    [SerializeField] 
    private Transform _cameraT;
    [SerializeField] 
    private float _speed = 1f;
    [SerializeField] 
    private float _mouseSensitivity = 90f;

    [SerializeField] 
    private float _gravity = -1.63f;
    [SerializeField] 
    private Transform _groundCheck;
    [SerializeField] 
    private float _groundDistance = 0.4f;
    [SerializeField] 
    private LayerMask _groundMask;
    [SerializeField] 
    private float _jumpHeight = 3f;


    private Animator _animator;
    private CharacterController _characterController;

    private Vector3 _inputVector;
    private float _inputSpeed;
    private Vector3 _targetDirection;
    private bool _isJumping = false;
    private bool _isRunning = false;

    private float cameraXRotation = 0f;
    private Vector3 _velocity;
    private bool _isGrounded;


    void Start() {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update() {
        //Ground Check
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftShift)) _speed = 2f;
            if (Input.GetKeyUp(KeyCode.LeftShift)) _speed = 1f;

        }

        if (_isGrounded)
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

            //Compute direction According to Camera Orientation
            transform.Rotate(Vector3.up, mouseX);
            cameraXRotation -= mouseY;
            cameraXRotation = Mathf.Clamp(cameraXRotation, -90f, 90f);
            _cameraT.localRotation = Quaternion.Euler(cameraXRotation, 0f, 0f);
        }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 move = (transform.right * h + transform.forward * v).normalized;
            _characterController.Move(move * _speed * Time.deltaTime);
            _inputVector = new Vector3(h, 0, v);
            _inputSpeed = Mathf.Clamp(_inputVector.magnitude, 0f, 1f);


            if (Input.GetKey(KeyCode.LeftShift))
                _speed = 3f;
            

            if (Input.GetKeyUp(KeyCode.LeftShift))
                _speed = 1f;
            
        
             if (Input.GetKey(KeyCode.Space) && _isGrounded && _isJumping == false)
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);

        //FALLING
        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

        UpdateAnimations();
    }


    private void UpdateAnimations() {
        _animator.SetFloat("speed", _inputSpeed);
        _animator.SetBool("run", Input.GetKey(KeyCode.LeftShift));
        _animator.SetBool("jump", Input.GetKey(KeyCode.Space));
    }

}
