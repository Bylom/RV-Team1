using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{

    public Inventory _Inventory;
    public int counter = 0;

    public void OnItemClicked()
    {
        ItemDragHandler dragHandler = gameObject.transform.Find("Image").GetComponent<ItemDragHandler>();

        IInventoryItem item = dragHandler.Item;

        Debug.Log(item.Name);

        transform.parent = null;

        _Inventory.UseItem(item);

        item.OnUse();

        counter++;
    }

}
