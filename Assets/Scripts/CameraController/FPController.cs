using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Collections;
using General;
using GeneralUI;
using UnityEngine.SceneManagement;

public class FPController : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private Transform cameraT;

    [SerializeField] private float speed = 1f;

    [SerializeField] private float mouseSensitivity = 90f;

    [SerializeField] private float gravity = -1.63f;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private float groundDistance = 0.6f;

    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float jumpHeight = 2f;

    [SerializeField] private float minX = -60f;
    [SerializeField] private float maxX = 30f;

    [SerializeField] private GameState gameState;

    private Animator m_Animator;
    private CharacterController m_CharacterController;

    private Vector3 m_InputVector;
    private float m_InputSpeed;
    private Vector3 m_TargetDirection;
    private bool m_IsJumping;
    private bool m_isTaking;

    private float m_CameraXRotation;
    private Vector3 m_Velocity;
    private bool m_IsGrounded;
    private Vector3 m_Inertia;
    [SerializeField] private GameObject sasso;
    [SerializeField] private GameObject non_sasso;
    [SerializeField] private bool canTake = true;
    [SerializeField] private DialogueTrigger finalDialogue;

    public Inventory inventory;
    [FormerlySerializedAs("Hand")] public GameObject hand;
    [FormerlySerializedAs("leftHand")] public GameObject leftHand;
    [FormerlySerializedAs("NearObject")] public bool nearObject = false;
    [FormerlySerializedAs("Canvas")] public GameObject canvas;
    public bool firstUse = false;
    public bool Slot1 = false;
    public bool throw_obj = false;
    public Text press;
    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Jump = Animator.StringToHash("jump");
    private static readonly int Take = Animator.StringToHash("take");

    public GameObject Palla;
    public GameObject Mazza;
    [SerializeField] private bool canJump = true;


    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        inventory.ItemUsed += Inventory_ItemUsed;
        inventory.ItemRemoved += Inventory_ItemRemoved;
        if (!(sasso is null))
            sasso.SetActive(false);
        if (!(non_sasso is null))
            non_sasso.SetActive(false);
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;

        GameObject goItem = (item as MonoBehaviour)?.gameObject;

        if (!(goItem is null))
        {
            goItem.SetActive(true);

            goItem.transform.parent = null;
        }
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;

        GameObject goItem = (item as MonoBehaviour)?.gameObject;

        if (!(goItem is null))
        {
            if (Slot1 == true)
            {
                goItem.SetActive(true);
                goItem.transform.parent = hand.transform;
                //mCurrentItem1 = e.Item;
            }

            if (Slot1 == false)
            {
                goItem.SetActive(true);
                goItem.transform.parent = leftHand.transform;
                //mCurrentItem = e.Item;
                Slot1 = true;
            }
        }

        mCurrentItem = e.Item;
    }

    private IInventoryItem mCurrentItem = null;

    private bool mLockPickup;
    private static readonly int Flag = Animator.StringToHash("Flag");

    private void DropCurrentItem()
    {
        mLockPickup = true;

        m_Animator.SetBool(Flag, true);

        GameObject goItem = (mCurrentItem as MonoBehaviour)?.gameObject;

        inventory.RemoveItem(mCurrentItem);


        var forward = transform.forward;
        Palla.GetComponent<Palla>().Golf.AddForce(forward * 0.30f, ForceMode.Impulse);
        Palla.GetComponent<Palla>().Golf.isKinematic = false;
        Palla.GetComponent<Palla>().coll_Golf.enabled = true;
        Palla.transform.parent = null;
        Mazza.GetComponent<Mazza>().Mazza_Golf.AddForce(forward * 0.30f, ForceMode.Impulse);
        Mazza.GetComponent<Mazza>().Mazza_Golf.isKinematic = false;
        Mazza.GetComponent<Mazza>().coll_Mazza.enabled = true;
        Mazza.transform.parent = null;

        Invoke("DoDropItem", 0.25f);

        StartCoroutine("WaitForSec");
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        FindObjectOfType<DialogueManager>().endScene = true;
        finalDialogue.TriggerDialogue();
    }

    public void DoDropItem()
    {
        mLockPickup = false;
        mCurrentItem = null;
    }

    void FixedUpdate()
    {
        if (mCurrentItem != null && Input.GetKey(KeyCode.Mouse1) && throw_obj)
        {
            DropCurrentItem();
        }
    }

    void Update()
    {
        //Ground Check
        var position = groundCheck.position;
        var position1 = cameraT.position;
        m_IsGrounded = Physics.Linecast(position1, position, groundMask);
        Debug.DrawLine(position1, position, Color.red, 0, false);
        if (m_IsGrounded && m_Velocity.y < 0f)
        {
            m_Velocity.y = -2f;
            if (Input.GetKey(KeyCode.LeftShift)) speed = 2f;
            if (Input.GetKeyUp(KeyCode.LeftShift)) speed = 1f;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Compute direction According to Camera Orientation
        transform.Rotate(Vector3.up, mouseX);
        m_CameraXRotation -= mouseY;
        m_CameraXRotation = Mathf.Clamp(m_CameraXRotation, minX, maxX);
        cameraT.localRotation = Quaternion.Euler(m_CameraXRotation, 0f, 0f);
        if (gameState.GetPaused())
        {
            m_Animator.SetBool(Run, false);
            m_Animator.SetBool(Jump, false);
            m_Animator.SetFloat(Speed, 0);
            return;
        }

        if (m_IsGrounded)
        {
            m_IsJumping = false;
        }


        if (Input.GetKey(KeyCode.LeftShift) && m_IsGrounded)
        {
            speed = 2f;
        }
        else if (m_IsGrounded && Math.Abs(speed - 1f) > 0.01f)
        {
            speed = 1f;
        }

        if (!m_IsJumping)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            var transform1 = transform;
            m_Inertia = (transform1.right * h + transform1.forward * v).normalized;

            m_CharacterController.Move(m_Inertia * (speed * Time.deltaTime));
            m_InputVector = new Vector3(h, 0, v);
            m_InputSpeed = Mathf.Clamp(m_InputVector.magnitude, 0f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && m_IsGrounded && !m_IsJumping && canJump && !gameState.GetPaused())
        {
            m_Velocity.y = jumpHeight;
            m_IsJumping = true;
        }

        if (m_IsJumping)
        {
            m_CharacterController.Move(m_Inertia * (speed * Time.deltaTime));
        }


        //FALLING
        m_Velocity.y += gravity * Time.deltaTime;
        m_CharacterController.Move(m_Velocity * Time.deltaTime);

        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        m_Animator.SetFloat(Speed, !m_IsJumping ? m_InputSpeed : 0f);
        m_Animator.SetBool(Run, Input.GetKey(KeyCode.LeftShift) && !m_IsJumping && Math.Abs(m_InputSpeed) > 0.01f);
        m_Animator.SetBool(Jump, m_IsJumping);
        if (canTake)
            m_Animator.SetBool(Take, m_isTaking);
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            var inventoryVar = canvas.GetComponent<InventorySlot>();
            if (!(inventoryVar is null))
                inventoryVar.NearInventory = true;
        }

        if (collision.gameObject.CompareTag("Sphere"))
        {
            throw_obj = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            nearObject = false;

            var inventoryVar = canvas.GetComponent<InventorySlot>();
            if (!(inventoryVar is null))
                inventoryVar.NearInventory = false;
        }

        if (collision.gameObject.CompareTag("Sphere"))
        {
            throw_obj = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (mLockPickup)
            return;

        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        if (item != null)
        {
            nearObject = true;
            inventory.AddItem(item);
            item.OnPickup();
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.CompareTag("Rock") || coll.gameObject.CompareTag("Palla"))
        {
            if ((!sasso.activeSelf && !non_sasso.activeSelf) && Input.GetKey(KeyCode.E))
            {
                m_isTaking = true;
                UpdateAnimations();
                StartCoroutine(ExampleCoroutine(coll));
            }
            else
                m_isTaking = false;
        }
    }

    IEnumerator ExampleCoroutine(Collider rock)
    {
        yield return new WaitForSeconds(2);
        if (rock.gameObject.CompareTag("Palla"))
        {
            rock.gameObject.SetActive(false);
            non_sasso.SetActive(true);
        }
        else
        {
            rock.gameObject.SetActive(false);
            sasso.SetActive(true);
        }
    }
}