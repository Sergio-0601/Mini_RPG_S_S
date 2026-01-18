using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Inventory Settings")]
    [SerializeField] private int inventorySize = 20;
    
    private List<InventorySlot> inventory;
    private bool hasBackpack = false; // Para la Misión 1

    // Eventos para actualizar UI
    public event Action<List<InventorySlot>> OnInventoryChanged;
    public event Action OnBackpackObtained;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeInventory();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeInventory()
    {
        inventory = new List<InventorySlot>();
        for (int i = 0; i < inventorySize; i++)
        {
            inventory.Add(new InventorySlot());
        }
    }

    public bool HasBackpack()
    {
        return hasBackpack;
    }

    public void ObtainBackpack()
    {
        hasBackpack = true;
        OnBackpackObtained?.Invoke();
        Debug.Log("¡Has obtenido la mochila! Ahora puedes guardar objetos.");
    }

    public bool AddItem(Item item, int quantity = 1)
    {
        if (!hasBackpack && item.itemType != Item.ItemType.QuestItem)
        {
            Debug.Log("Necesitas una mochila para guardar esto.");
            return false;
        }

        // Si es la mochila misma
        if (item.itemName == "Mochila")
        {
            ObtainBackpack();
            return true;
        }

        // Buscar slot existente si es stackable
        if (item.isStackable)
        {
            foreach (var slot in inventory)
            {
                if (!slot.IsEmpty() && slot.item == item && slot.quantity < item.maxStackSize)
                {
                    int amountToAdd = Mathf.Min(quantity, item.maxStackSize - slot.quantity);
                    slot.AddItem(item, amountToAdd);
                    quantity -= amountToAdd;
                    
                    OnInventoryChanged?.Invoke(inventory);
                    
                    if (quantity <= 0)
                        return true;
                }
            }
        }

        // Buscar slot vacío
        while (quantity > 0)
        {
            InventorySlot emptySlot = FindEmptySlot();
            if (emptySlot != null)
            {
                int amountToAdd = item.isStackable ? Mathf.Min(quantity, item.maxStackSize) : 1;
                emptySlot.AddItem(item, amountToAdd);
                quantity -= amountToAdd;
            }
            else
            {
                Debug.Log("Inventario lleno!");
                return false;
            }
        }

        OnInventoryChanged?.Invoke(inventory);
        return true;
    }

    public bool RemoveItem(Item item, int quantity = 1)
    {
        int remainingToRemove = quantity;

        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            if (!inventory[i].IsEmpty() && inventory[i].item == item)
            {
                if (inventory[i].quantity >= remainingToRemove)
                {
                    inventory[i].RemoveItem(remainingToRemove);
                    OnInventoryChanged?.Invoke(inventory);
                    return true;
                }
                else
                {
                    remainingToRemove -= inventory[i].quantity;
                    inventory[i].Clear();
                }
            }
        }

        OnInventoryChanged?.Invoke(inventory);
        return remainingToRemove <= 0;
    }

    public int GetItemCount(Item item)
    {
        int count = 0;
        foreach (var slot in inventory)
        {
            if (!slot.IsEmpty() && slot.item == item)
            {
                count += slot.quantity;
            }
        }
        return count;
    }

    public bool HasItem(Item item, int requiredQuantity = 1)
    {
        return GetItemCount(item) >= requiredQuantity;
    }

    public void UseItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= inventory.Count)
            return;

        InventorySlot slot = inventory[slotIndex];
        if (slot.IsEmpty() || !slot.item.isUsable)
            return;

        // Usar el item
        PlayerManager player = FindFirstObjectByType<PlayerManager>();
        if (player != null)
        {
            slot.item.Use(player);
            slot.RemoveItem(1);
            OnInventoryChanged?.Invoke(inventory);
        }
    }

    private InventorySlot FindEmptySlot()
    {
        foreach (var slot in inventory)
        {
            if (slot.IsEmpty())
                return slot;
        }
        return null;
    }

    public List<InventorySlot> GetInventory()
    {
        return inventory;
    }

    public InventorySlot GetSlot(int index)
    {
        if (index >= 0 && index < inventory.Count)
            return inventory[index];
        return null;
    }
}