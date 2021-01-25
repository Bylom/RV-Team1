using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {

        private const int Slots = 6;
        private List<IInventoryItem> mItems = new List<IInventoryItem>();

        public event EventHandler<InventoryEventArgs> ItemAdded;

        public event EventHandler<InventoryEventArgs> ItemRemoved;

        public event EventHandler<InventoryEventArgs> ItemUsed;

        public void AddItem(IInventoryItem item)
        {
            if (mItems.Count < Slots)
            {
                Collider itemCollider = (item as MonoBehaviour)?.GetComponent<Collider>();
                if (!(itemCollider is null) && itemCollider.enabled)
                {
                    itemCollider.enabled = false;
                    mItems.Add(item);
                    item.OnPickup();

                    ItemAdded?.Invoke(this, new InventoryEventArgs(item));
                }
            }
        }

        internal void UseItem(IInventoryItem item)
        {
            if (ItemUsed != null)
            {
                ItemUsed(this, new InventoryEventArgs(item));
            }
        }

        public void RemoveItem(IInventoryItem item)
        {
            if (mItems.Contains(item))
            {
                mItems.Remove(item);
                item.OnDrop();

                Collider itemCollider = (item as MonoBehaviour)?.GetComponent<Collider>();
                if (itemCollider != null)
                {
                    Debug.Log("Niente");
                    itemCollider.enabled = true;
                }

                ItemRemoved?.Invoke(this, new InventoryEventArgs(item));
            }
        }
    }
}
