using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palla : InventoyItemBase
{

    public Rigidbody Golf;
    public Collider coll_Golf;

    private void Start()
    {
        Golf = GetComponent<Rigidbody>();
        coll_Golf = GetComponent<Collider>();
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