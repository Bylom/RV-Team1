using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
   
    public float smoothFactor = 100f;

    private float horizontalInput;
    private float verticalInput;
    private float m_CameraXRotation;
    private float m_CameraYRotation;

    [SerializeField] private Transform cameraT;
    [SerializeField] public Camera camera1;
    [SerializeField] public Camera camera2;
    [SerializeField] private float mouseSensitivity = 120f;
    [SerializeField] private GameObject GroundCheck;

    public Vector3 targetPosition;

    private Animator m_Animator;
    private CharacterController m_CharacterController;
    public GameObject astronaut;

    private bool m_Down;
    private bool m_Step;
    private bool m_Wait;
    private bool shouldMove;
    private bool lookDown;

    private static readonly int Down = Animator.StringToHash("down");
    private static readonly int Step = Animator.StringToHash("step");

    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
        shouldMove = true;
    }

    void Update()
    {
        //Camera
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        m_CameraXRotation -= mouseY;
        m_CameraYRotation += mouseX;

        if (lookDown)
        {
            camera2.enabled = true;
            camera1.enabled = false;
        }
        else
        {
            camera1.enabled = true;
            camera2.enabled = false;
        }

        m_CameraXRotation = Mathf.Clamp(m_CameraXRotation, -30, 40);
        m_CameraYRotation = Mathf.Clamp(m_CameraYRotation, 15, 125);
        cameraT.localRotation = Quaternion.Euler(m_CameraXRotation, m_CameraYRotation, 0f);

        //Movement
        if (shouldMove)
        {
            float translation = Input.GetAxis("Vertical");
            translation *= Time.deltaTime / 2;
            transform.Translate(0, translation, 0);
        }

        //Animation
        if (shouldMove && Input.GetKey(KeyCode.S))
        {
            m_Down = true;
            UpdateAnimations();
        }

    }

    private void UpdateAnimations()
    {
        m_Animator.SetBool(Down, m_Down);
        m_Animator.SetBool(Step, m_Step);
    }

    private void OnTriggerStay(Collider coll)
    {

        if (coll.gameObject.CompareTag("Check"))
        {
            m_Down = false;
            transform.Translate(0, 0, 0);
            shouldMove = false;
            MakeFirstStep(coll);
        }
    }

    private void MakeFirstStep(Collider coll)
    {
        //trigger dialogue: "you can make the first step!"
        //audio manager: surface

       

        if (Input.GetKey(KeyCode.E))
        {
            //camera switch
            lookDown = true;
        }

        if (lookDown && Input.GetKey(KeyCode.S))
        {
            coll.gameObject.SetActive(false);
            m_Step = true;
            UpdateAnimations();
            Attendi();
            //audio: one step for a man
            //END
        }

    }

    private void OnCollisionEnter(Collision ground)
    {
        if (ground.gameObject.CompareTag("Ground"))
        {
            m_Down = false;
            transform.Translate(0, 0, 0);
            shouldMove = false;
            
        }
    }

    private void Attendi()
    {
        shouldMove = true;
        Debug.Log("hellooooo");
        float translation = Input.GetAxis("Vertical");
        translation *= Time.deltaTime * 1000;
        Debug.Log(translation);
        transform.Translate(0, translation, 0);
    }


  
}
