using UnityEngine;

namespace CameraController
{
    public class CarController : MonoBehaviour
    {
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";

        private float horizontalInput;
        private float verticalInput;
        private float currentSteerAngle;
        private float currentbreakForce;
        private bool isBreaking;

        public Rigidbody target;
        private float speed = 0.0f;
        private int fermo = 0;

        [SerializeField] private float prima;
        [SerializeField] private float seconda;
        [SerializeField] private float terza;

        private float motorForce;
        [SerializeField] private float breakForce;
        [SerializeField] private float maxSteerAngle;

        [SerializeField] private WheelCollider frontLeftWheelCollider;
        [SerializeField] private WheelCollider frontRightWheelCollider;
        [SerializeField] private WheelCollider rearLeftWheelCollider;
        [SerializeField] private WheelCollider rearRightWheelCollider;

        [SerializeField] private Transform frontLeftWheelTransform;
        [SerializeField] private Transform frontRightWheeTransform;
        [SerializeField] private Transform rearLeftWheelTransform;
        [SerializeField] private Transform rearRightWheelTransform;


        private void Start()
        {
            FindObjectOfType<AudioManager>().Play("Crater");
        }
        private void FixedUpdate()
        {
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }


        private void GetInput()
        {
            horizontalInput = Input.GetAxis(HORIZONTAL);
            verticalInput = Input.GetAxis(VERTICAL);
            isBreaking = Input.GetKey(KeyCode.Space);
            speed = target.velocity.magnitude;
        }

        private void HandleMotor()
        {
            // Debug.Log(speed);

            if (speed < 5)
            {
                motorForce = prima;
                // Debug.Log("prima! ");
            }


            if (speed > 5 && speed < 13)
            {
                motorForce = seconda;
                // Debug.Log("seconda! ");
            }


            if (speed > 13)
            {
                motorForce = terza;
                //  Debug.Log("terza! ");
            }



            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
            currentbreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();

            if (speed < 1.0f && Input.GetKey(KeyCode.W))
                fermo++;

            if (fermo == 1000)
            {
                Debug.Log("SOS!\n");
                //trigger dialogue!
                fermo = 0;
            }



        }

        private void ApplyBreaking()
        {
            frontRightWheelCollider.brakeTorque = currentbreakForce;
            frontLeftWheelCollider.brakeTorque = currentbreakForce;
            rearLeftWheelCollider.brakeTorque = currentbreakForce;
            rearRightWheelCollider.brakeTorque = currentbreakForce;
        }

        private void HandleSteering()
        {
            currentSteerAngle = maxSteerAngle * horizontalInput;
            frontLeftWheelCollider.steerAngle = currentSteerAngle;
            frontRightWheelCollider.steerAngle = currentSteerAngle;
        }

        private void UpdateWheels()
        {
            UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
            UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
            UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
            UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        }

        private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Vector3 pos;
            Quaternion rot
                ; wheelCollider.GetWorldPose(out pos, out rot);
            wheelTransform.rotation = rot;
            wheelTransform.position = pos;
        }

        
    }

}
