using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotsContainer;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private GameObject itemInfoPanel;

    private List<InventorySlotUI> slotUIList = new List<InventorySlotUI>();
    private bool isOpen = false;
 

    private void Start()
    {
        CreateSlots();
        
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged += UpdateUI;
        }

        inventoryPanel.SetActive(false); // Ensure hidden on start
        if (itemInfoPanel != null)
            itemInfoPanel.SetActive(false);
    }
 
    private void CreateSlots()
    {
        if (InventoryManager.Instance == null)
            return;

        var inventory = InventoryManager.Instance.GetInventory();
        
        foreach (Transform child in slotsContainer)
        {
            Destroy(child.gameObject);
        }
        slotUIList.Clear();

        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsContainer);
            InventorySlotUI slotUI = slotObj.GetComponent<InventorySlotUI>();
            
            if (slotUI != null)
            {
                slotUI.SetSlotIndex(i);
                slotUI.OnSlotClicked += HandleSlotClick;
                slotUIList.Add(slotUI);
            }
        }

        UpdateUI(inventory);
    }

    private void UpdateUI(List<InventorySlot> inventory)
    {
        for (int i = 0; i < slotUIList.Count && i < inventory.Count; i++)
        {
            slotUIList[i].UpdateSlot(inventory[i]);
        }
    }

    private void HandleSlotClick(int slotIndex)
    {
        if (InventoryManager.Instance == null)
            return;

        InventorySlot slot = InventoryManager.Instance.GetSlot(slotIndex);
        
        if (slot != null && !slot.IsEmpty())
        {
            ShowItemInfo(slot.item);

            if (slot.item.isUsable)
            {
                InventoryManager.Instance.UseItem(slotIndex);
            }
        }
        else
        {
            HideItemInfo();
        }
    }

    private void ShowItemInfo(Item item)
    {
        if (itemInfoPanel != null)
        {
            itemInfoPanel.SetActive(true);
            itemNameText.text = item.itemName;
            itemDescriptionText.text = item.description;
        }
    }

    private void HideItemInfo()
    {
        if (itemInfoPanel != null)
        {
            itemInfoPanel.SetActive(false);
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
        
        if (!isOpen)
        {
            HideItemInfo();
        }
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged -= UpdateUI;
        }
    }
}
