using General;
using UnityEngine;

namespace Golf
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameState gameState;
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private GameObject astronaut;
        [SerializeField] private Transform target;
        [SerializeField] private PowerBarController powerBar;
        [SerializeField] private Rigidbody ballRigidBody;
        [SerializeField] private GameObject ball;
        [SerializeField] private Animator astronautAnimator;
        [SerializeField] private float distance = 5.0f;
        [SerializeField] private float xSpeed = 120.0f;
        [SerializeField] private float ySpeed = 120.0f;
        private Vector3 _targetPosition;

        [SerializeField] private float yMinLimit = -20f;
        [SerializeField] private float yMaxLimit = 80f;

        [SerializeField] private float distanceMin = .5f;
        [SerializeField] private float distanceMax = 15f;
        [SerializeField] private float sensitivity = 90f;

        private Rigidbody _rigidBody;
        private float _powerValue;
        [SerializeField] private float barIncrement = 1;
        [SerializeField] private float strength = 50;
        private readonly int _collisionIterationLimit = 200;
        private float _x;
        private float _y;
        private float _astronautDistance;
        private static readonly int Hit = Animator.StringToHash("Hit");

        // Use this for initialization
        void Start()
        {
            if (target)
            {
                _targetPosition = target.position;
            }

            _astronautDistance = Vector3.Distance(astronaut.transform.position, _targetPosition);

            Cursor.lockState = CursorLockMode.Locked;
            Vector3 angles = transform.eulerAngles;
            _x = angles.y;
            _y = angles.x;

            _rigidBody = GetComponent<Rigidbody>();

            // Make the rigid body not change rotation
            if (_rigidBody != null)
            {
                _rigidBody.freezeRotation = true;
            }
        }

        private void Update()
        {
            if (gameState.GetPaused()) return;
            if (powerBar is null) return;
            var value = Input.GetAxisRaw("Vertical");
            if (value < 0)
            {
                _powerValue -= barIncrement * Time.deltaTime;
                if (_powerValue < 0)
                    _powerValue = 0;
            }
            else if (value > 0)
            {
                _powerValue += barIncrement * Time.deltaTime;
                if (_powerValue > 1)
                    _powerValue = 1;
            }

            powerBar.setValue(_powerValue);
            if (Input.GetButtonUp("Fire1") && !(ballRigidBody is null) && _powerValue > 0)
            {
                astronautAnimator.SetBool(Hit, true);
                gameState.SetPaused(true);
            }

            if (Input.GetButtonUp("Fire2") && (ballRigidBody is null))
            {
                ball = Instantiate(ballPrefab, _targetPosition, Quaternion.identity);
                ballRigidBody = ball.GetComponent<Rigidbody>();
                ball.GetComponent<Ball>().originPoint = _targetPosition;
            }
        }

        public void HitBall()
        {
            ballRigidBody.isKinematic = false;
            Vector3 forceDirection = transform.forward;
            forceDirection.y = 0;
            forceDirection = Vector3.Normalize(forceDirection);
            forceDirection.y = 1;
            forceDirection = Vector3.Normalize(forceDirection);
            ballRigidBody.AddForce(forceDirection * (_powerValue * strength), ForceMode.Impulse);
            ballRigidBody = null;
            Destroy(ball, 40);
            ball = null;
        }

        void LateUpdate()
        {
            if (!target || gameState.GetPaused()) return;

            _x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime * sensitivity;
            _y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime * sensitivity;

            _y = ClampAngle(_y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(_y, _x, 0);
            Quaternion astronautRotation = Quaternion.Euler(0, _x + 90, 0);
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            var negDistance = new Vector3(0.0f, 0.0f, -distance);
            var astronautNegDistance = new Vector3(0.0f, 0.0f, -_astronautDistance);

            var position = rotation * negDistance + _targetPosition;
            var astronautPosition = astronautRotation * astronautNegDistance + _targetPosition;

            var destinationCollisionVec = new Vector3(position.x, position.y -1, position.z);

            var limit = 0;
            //Adjustment of y to not collide with ground 
            // var tmpTargetPosition = new Vector3(_targetPosition.x, _targetPosition.y + 0.2f, _targetPosition.z);
            while (Physics.Linecast(_targetPosition, destinationCollisionVec, out _)  && limit < _collisionIterationLimit)
            {
                destinationCollisionVec.y += 0.01f;
                position.y += 0.01f;
                limit += 1;
            }

            var tmpVec = new Vector3(astronautPosition.x, astronautPosition.y, astronautPosition.z);
            limit = 0;
            while (!Physics.Linecast(astronautPosition, tmpVec, out _) && limit < _collisionIterationLimit)
            {
                tmpVec.y -= 0.01f;
                limit += 1;
            }
            if (limit == _collisionIterationLimit)
            {
                limit = 0;
                tmpVec = new Vector3(astronautPosition.x, astronautPosition.y, astronautPosition.z);
                while (!Physics.Linecast(astronautPosition, tmpVec, out _) && limit < _collisionIterationLimit)
                {
                    tmpVec.y += 0.01f;
                    limit += 1;
                }

                if (limit == _collisionIterationLimit)
                    tmpVec.y = astronautPosition.y;
            }

            tmpVec.y += 0.01f;
            astronautPosition = tmpVec;

            var transform1 = transform;
            transform1.rotation = rotation;
            transform1.position = position;

            var transformAstronaut = astronaut.transform;
            transformAstronaut.rotation = astronautRotation;
            transformAstronaut.position = astronautPosition;

            Debug.DrawLine(_targetPosition, transformAstronaut.position, Color.red, 3f);
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