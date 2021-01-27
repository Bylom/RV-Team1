using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraXRover : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private Transform cameraT;
    [SerializeField] private float mouseSensitivity = 90f;

    private float m_CameraXRotation;

    void Start() { }

        void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up, mouseX);
        m_CameraXRotation -= mouseY;
        m_CameraXRotation = Mathf.Clamp(m_CameraXRotation, -60f, 30f);
        cameraT.localRotation = Quaternion.Euler(m_CameraXRotation, 0f, 0f);
    }
}

    