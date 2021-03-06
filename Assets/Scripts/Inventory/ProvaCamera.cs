﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvaCamera : MonoBehaviour
{
    public Transform cameraTarget1;
    public Transform cameraTarget2;
    public Transform cameraTarget3;
    public GameObject Counter;
    public float sSpeed = 1.0f;
    public Vector3 dist;
    public Transform lookTarget;

    private Transform cameraTarget;

    public Inventory_Opener mov;
    public FPController cam;

    void Update()
    {
        if (mov.closed && mov.NearInvent)
        {
            if (Input.GetKey(KeyCode.E))
            {
                cam.enabled = false;
                cameraTarget = cameraTarget3.transform;
                StartCoroutine("WaitForSec");
                
            }
        }
        if (mov.open)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (Counter.GetComponent<Counter>().go == true)
                {
                    cam.enabled = true;
                    cameraTarget = cameraTarget2.transform;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (cameraTarget is null) return;
        Vector3 dPos = cameraTarget.position + dist;
        Vector3 sPos = Vector3.Lerp(transform.position, dPos, sSpeed * Time.deltaTime);
        transform.position = sPos;
        transform.LookAt(lookTarget.position);
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        sSpeed = 5.0f;
        cameraTarget = cameraTarget1.transform;
    }

}
