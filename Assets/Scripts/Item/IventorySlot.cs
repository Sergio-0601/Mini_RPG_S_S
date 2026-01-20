using System;
using UnityEngine;
[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int quantity;
    public InventorySlot()
    {
        item = null;
        quantity = 0;
    }
    public InventorySlot(Item newItem, int amount)
    {
        item = newItem;
        quantity = amount;
    }
    public bool IsEmpty()
    {
        return item == null || quantity <= 0;
    }
    public bool CanAddItem(Item itemToAdd)
    {
        if (IsEmpty()) return true;
        if (item == itemToAdd && item.isStackable && quantity < item.maxStackSize)
            return true;
        return false;
    }
    public void AddItem(Item newItem, int amount = 1)
    {
        if (IsEmpty())
        {
            item = newItem;
            quantity = amount;
        }
        else if (item == newItem && item.isStackable)
        {
            quantity += amount;
            if (quantity > item.maxStackSize)
                quantity = item.maxStackSize;
        }
    }
    public void RemoveItem(int amount = 1)
    {
        quantity -= amount;
        if (quantity <= 0)
        {
            item = null;
            quantity = 0;
        }
    }
    public void Clear()
    {
        item = null;
        quantity = 0;
    }
}
