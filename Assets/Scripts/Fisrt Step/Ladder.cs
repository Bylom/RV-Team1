using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GeneralUI;
using UnityEngine.SceneManagement;

public class Ladder : MonoBehaviour
{
    public GameObject armature;
    public Image BlackIm;

    private float horizontalInput;
    private float verticalInput;
    private float m_CameraXRotation;
    private float m_CameraYRotation;

    [SerializeField] private Transform cameraT;
    [SerializeField] public Camera camera1;
    [SerializeField] public Camera camera2;
    [SerializeField] private float mouseSensitivity = 120f;
    [SerializeField] private GameObject GroundCheck;

    private Animator m_Animator;
    private CharacterController m_CharacterController;

    private bool m_Down;
    private bool m_Step;
    private bool m_Wait;
    private bool change;
    private static readonly int Down = Animator.StringToHash("down");
    private static readonly int Step = Animator.StringToHash("step");

    public Dialogue dialogue;

    public DialogueTrigger dialogueTrigger;

    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Camera
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        m_CameraXRotation -= mouseY;
        m_CameraYRotation += mouseX;

        m_CameraXRotation = Mathf.Clamp(m_CameraXRotation, -30, 40);
        m_CameraYRotation = Mathf.Clamp(m_CameraYRotation, -60, 60);
        cameraT.localRotation = Quaternion.Euler(m_CameraXRotation, m_CameraYRotation, 0f);

        if (!change)
        {
            camera1.enabled = true;
            camera2.enabled = false;
        }
        else
        {
            camera2.enabled = true;
            camera1.enabled = false;
        }


        //Animation
        if (Input.GetKey(KeyCode.S))
        {
            m_Down = true;
            UpdateAnimations();
        }

        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Ladder_down") && (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
        {
          //  FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }


        if (Input.GetKey(KeyCode.E))
        {
            change = true;
           
            StartCoroutine(MakeFirstStep());
            FindObjectOfType<AudioManager>().Play("FirstStep");
        }  

    }
    

    private void UpdateAnimations()
    {
        camera1.transform.parent = armature.transform;
        m_Animator.SetBool(Down, m_Down);
        m_Animator.SetBool(Step, m_Step);
    }
    


    IEnumerator MakeFirstStep()
    {
        yield return new WaitForSeconds(1);
        m_Step = true;
        UpdateAnimations();
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.endScene = true;
        dialogueTrigger.SetDialog(new Dialogue()
        {
            name = "Mission control",
            sentences = new[]
            {
                        "Good job, you reached the ball",
                        "Those minerals look interesting, take some samples",
                        "And don't forget the ball!"
                    }
        });

        dialogueTrigger.TriggerDialogue();
        //SceneManager.LoadScene("Scenes/Missioni");
        Cursor.lockState = CursorLockMode.None;

        yield return new WaitForSeconds(3);
        BlackIm.CrossFadeAlpha(1, 2, false);
        
    }
        




}
