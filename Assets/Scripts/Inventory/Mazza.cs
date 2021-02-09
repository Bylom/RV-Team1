using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mazza : InventoyItemBase
{

    public Rigidbody Mazza_Golf;
    public Collider coll_Mazza;

    private void Start()
    {
        Mazza_Golf = GetComponent<Rigidbody>();
        coll_Mazza = GetComponent<Collider>();
    }

    public override string Name
    {
        get
        {
            return "Mazza";
        }
    }


    public override void OnUse()
    {
        base.OnUse();
    }

}