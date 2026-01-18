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
        Debug.Log("=== InventoryUI Start ===");
        Debug.Log($"inventoryPanel: {inventoryPanel?.name}");
        Debug.Log($"slotPrefab: {slotPrefab?.name}");
        Debug.Log($"slotsContainer: {slotsContainer?.name}");
        
        CreateSlots();
        
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged += UpdateUI;
            Debug.Log("InventoryManager encontrado");
        }
        else
        {
            Debug.LogError("¡InventoryManager.Instance es NULL!");
        }

        inventoryPanel.SetActive(false);
        if (itemInfoPanel != null)
            itemInfoPanel.SetActive(false);
            
        Debug.Log("InventoryUI inicializado correctamente");
    }

   private void Update()
{
    
    #if ENABLE_INPUT_SYSTEM
    var keyboard = UnityEngine.InputSystem.Keyboard.current;
    if (keyboard != null)
    {
        if (keyboard.tabKey.wasPressedThisFrame)
        {
            Debug.Log("¡¡¡TAB PRESIONADA!!!");
            ToggleInventory();
        }
        
        if (keyboard.iKey.wasPressedThisFrame)
        {
            Debug.Log("¡¡¡I PRESIONADA!!!");
            ToggleInventory();
        }
        
        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("¡¡¡SPACE PRESIONADA!!! (test)");
        }
    }
    #else
    
    if (Input.GetKeyDown(KeyCode.Tab))
    {
        Debug.Log("¡¡¡TAB PRESIONADA!!!");
        ToggleInventory();
    }
    
    if (Input.GetKeyDown(KeyCode.I))
    {
        Debug.Log("¡¡¡I PRESIONADA!!!");
        ToggleInventory();
    }
    
    if (Input.GetKeyDown(KeyCode.Space))
    {
        Debug.Log("¡¡¡SPACE PRESIONADA!!! (test)");
    }
    #endif
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
        Debug.Log("=== TOGGLE INVENTORY LLAMADO ===");
        Debug.Log($"isOpen ANTES: {isOpen}");
        Debug.Log($"inventoryPanel null? {inventoryPanel == null}");
        
        if (inventoryPanel == null)
        {
            Debug.LogError("¡inventoryPanel es NULL!");
            return;
        }
        
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
        
        Debug.Log($"isOpen DESPUÉS: {isOpen}");
        Debug.Log($"Panel activeSelf: {inventoryPanel.activeSelf}");
        
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
