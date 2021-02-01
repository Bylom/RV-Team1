using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FPController : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private Transform cameraT; 

    [SerializeField] private float speed = 1f;

    [SerializeField] private float mouseSensitivity = 90f;

    [SerializeField] private float gravity = -1.63f;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private float groundDistance = 0.4f;

    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float jumpHeight = 2f;

    private Animator m_Animator;
    private CharacterController m_CharacterController;

    private Vector3 m_InputVector;
    private float m_InputSpeed;
    private Vector3 m_TargetDirection;
    private bool m_IsJumping;

    private float m_CameraXRotation;
    private Vector3 m_Velocity;
    private bool m_IsGrounded;
    private Vector3 m_Inertia;


    public Inventory inventory;
    [FormerlySerializedAs("Hand")] public GameObject hand;
    [FormerlySerializedAs("NearObject")] public bool nearObject = false;
    [FormerlySerializedAs("Canvas")] public GameObject canvas;
    public Text press;
    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Jump = Animator.StringToHash("jump");

    public GameObject Palla;
    public GameObject Flag;

    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        inventory.ItemUsed += Inventory_ItemUsed;
        inventory.ItemRemoved += Inventory_ItemRemoved;
        Palla.GetComponent<Palla>().Golf.isKinematic = true;
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
            goItem.SetActive(true);

            goItem.transform.parent = hand.transform;
        }

        mCurrentItem = e.Item;
    }

    private IInventoryItem mCurrentItem = null;

    private bool mLockPickup = false;

    private void DropCurrentItem()
    {
        mLockPickup = true;

        m_Animator.SetBool("Flag", true);

        GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;

        inventory.RemoveItem(mCurrentItem);

        
        Palla.GetComponent<Palla>().Golf.AddForce(transform.forward * 2.0f, ForceMode.Impulse);
        Debug.Log("Lascia");
        Palla.GetComponent<Palla>().Golf.isKinematic = false;

        Invoke("DoDropItem", 0.25f);
    }

    public void DoDropItem()
    {
        mLockPickup = false;

        //Destroy((mCurrentItem as MonoBehaviour).GetComponent<Rigidbody>());

        mCurrentItem = null;
    }

    void FixedUpdate()
    {
        if (mCurrentItem != null && Input.GetKey(KeyCode.E))
        {
            DropCurrentItem();
        }
    }

    void Update()
    {
        //Ground Check
        m_IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (m_IsGrounded && m_Velocity.y < 0f)
        {
            m_Velocity.y = -2f;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftShift)) speed = 2f;
            if (Input.GetKeyUp(KeyCode.LeftShift)) speed = 1f;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Compute direction According to Camera Orientation
        transform.Rotate(Vector3.up, mouseX);
        m_CameraXRotation -= mouseY;
        m_CameraXRotation = Mathf.Clamp(m_CameraXRotation, -60f, 30f);
        cameraT.localRotation = Quaternion.Euler(m_CameraXRotation, 0f, 0f);

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

        if (Input.GetKey(KeyCode.Space) && m_IsGrounded && m_IsJumping == false)
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
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {

            Debug.Log("Ciao Cristian");
            //GetComponent<InventorySlot>().NearInventory = true;
            canvas.GetComponent<InventorySlot>().NearInventory = true;
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            nearObject = false;
            canvas.GetComponent<InventorySlot>().NearInventory = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (mLockPickup)
            return;

        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        if(item != null)
        {
            nearObject = true;
            inventory.AddItem(item);
            item.OnPickup();
        }
    }
}