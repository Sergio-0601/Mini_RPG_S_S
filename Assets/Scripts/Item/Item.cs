using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Consumable,
        Equipment,
        QuestItem,
        Collectable
    }
    [Header("Item Info")]
    public string itemName;
    [TextArea(3, 5)]
    public string description;
    public Sprite icon;
    public ItemType itemType;
    [Header("Stack")]
    public bool isStackable = false;
    public int maxStackSize = 1;
    [Header("Usage")]
    public bool isUsable = false;
    public int healAmount = 0;
    public virtual void Use(PlayerManager player)
    {
        if (itemType == ItemType.Consumable && healAmount > 0)
        {
            player.GetComponent<Health>().Heal(healAmount);
            Debug.Log($"Usaste {itemName} y recuperaste {healAmount} de vida");
        }
    }
}
