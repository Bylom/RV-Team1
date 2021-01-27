using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetEye : MonoBehaviour
{
    void Start()
    {
        if (!(Camera.main is null)) Camera.main.stereoTargetEye = StereoTargetEyeMask.Both;
    }
}