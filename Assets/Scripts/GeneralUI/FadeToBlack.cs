using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{ 
    public Image BlackIm;


    void Start()
    {
        BlackIm.CrossFadeAlpha(0, 1, false);
    }

    void FadeIn()
    {
        BlackIm.CrossFadeAlpha(1, 2, false);
    }

}
