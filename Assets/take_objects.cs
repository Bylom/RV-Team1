using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class take_objects : MonoBehaviour
{
    public Vector3 PickPosition;
    public Vector3 PickRotation;

    public bool isTake = false;

    private void Update()
    {
        if (isTake)
        {
            if (Input.GetKey(KeyCode.E))
            {
                transform.localPosition = PickPosition;
                transform.localEulerAngles = PickRotation;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTake = true;
            Debug.Log("Eccomi qua");
            Debug.Log(collision.name);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTake = false;
        }
    }
}
