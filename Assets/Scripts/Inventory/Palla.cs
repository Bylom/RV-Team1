using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palla : InventoyItemBase
{

    public Rigidbody Golf;

    private void Start()
    {
        Golf = GetComponent<Rigidbody>();
    }

    public override string Name
    {
        get
        {
            return "Palla";
        }
    }


    public override void OnUse()
    {
        base.OnUse();
    }

}