using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Elements")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private GameObject highlightBorder;

    private int slotIndex;
    private InventorySlot currentSlot;

    public event Action<int> OnSlotClicked;

    private void Start()
    {
        if (highlightBorder != null)
            highlightBorder.SetActive(false);
    }

    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }

    public void UpdateSlot(InventorySlot slot)
    {
        currentSlot = slot;

        if (slot.IsEmpty())
        {
            // Slot vacío
            itemIcon.enabled = false;
            quantityText.text = "";
            if (highlightBorder != null)
                highlightBorder.SetActive(false);
        }
        else
        {
            // Slot con item
            itemIcon.enabled = true;
            itemIcon.sprite = slot.item.icon;

            if (slot.item.isStackable && slot.quantity > 1)
            {
                quantityText.text = slot.quantity.ToString();
            }
            else
            {
                quantityText.text = "";
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSlotClicked?.Invoke(slotIndex);
    }

    public void Highlight(bool isHighlighted)
    {
        if (highlightBorder != null)
            highlightBorder.SetActive(isHighlighted);
    }
}