using UnityEngine;

namespace Golf
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        private Vector3 _targetPosition;
        public float distance = 5.0f;
        public float xSpeed = 120.0f;
        public float ySpeed = 120.0f;

        public float yMinLimit = -20f;
        public float yMaxLimit = 80f;

        public float distanceMin = .5f;
        public float distanceMax = 15f;
        public float sensitivity = 90f;
        private Rigidbody _rigidbody;

        private float _x;
        private float _y;

        // Use this for initialization
        void Start()
        {
            if (target)
            {
                _targetPosition = target.position;
                _targetPosition.y += 0.3f;
            }

            Cursor.lockState = CursorLockMode.Locked;
            Vector3 angles = transform.eulerAngles;
            _x = angles.y;
            _y = angles.x;

            _rigidbody = GetComponent<Rigidbody>();

            // Make the rigid body not change rotation
            if (_rigidbody != null)
            {
                _rigidbody.freezeRotation = true;
            }
        }

        void LateUpdate()
        {
            if (!target) return;

            _x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime * sensitivity;
            _y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime * sensitivity;

            _y = ClampAngle(_y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(_y, _x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + _targetPosition;

            if (Physics.Linecast(_targetPosition, position, out _) ||
                Physics.Linecast(transform.position, position, out _))
                return;

            var transform1 = transform;
            transform1.rotation = rotation;
            transform1.position = position;
        }

        private static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}