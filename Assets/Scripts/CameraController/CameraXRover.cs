using UnityEngine;

namespace CameraController
{
    public class CameraXRover : MonoBehaviour
    {
        public GameObject player;

        [SerializeField] private Transform cameraT;
        [SerializeField] private float mouseSensitivity = 90f;

        private float _mCameraXRotation;
        private float _mCameraYRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            
            transform.Rotate(Vector3.up, mouseX);
            _mCameraXRotation -= mouseY;
            _mCameraYRotation += mouseX;
            _mCameraXRotation = Mathf.Clamp(_mCameraXRotation, -60f, 30f);
            _mCameraYRotation = Mathf.Clamp(_mCameraYRotation, -27f, 27f);
            cameraT.localRotation = Quaternion.Euler(_mCameraXRotation, _mCameraYRotation, 0f);
        }
    }
}

    