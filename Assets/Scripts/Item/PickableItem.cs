using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [Header("Item Data")]
    [SerializeField] private Item item;
    [SerializeField] private int quantity = 1;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float bobSpeed = 1f;
    [SerializeField] private float bobHeight = 0.3f;

    [Header("Pickup Settings")]
    [SerializeField] private bool autoPickup = false;
    [SerializeField] private KeyCode pickupKey = KeyCode.E;

    private Vector3 startPosition;
    private bool playerInRange = false;
    private bool isPickedUp = false;

    private void Start()
    {
        startPosition = transform.position;
        
        if (spriteRenderer != null && item != null && item.icon != null)
        {
            spriteRenderer.sprite = item.icon;
        }
    }

    private void Update()
    {
        // Efecto de flotación
        if (!isPickedUp)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        // Pickup manual - Compatible con ambos sistemas de input
        if (playerInRange && !autoPickup)
        {
            #if ENABLE_INPUT_SYSTEM
            if (UnityEngine.InputSystem.Keyboard.current != null && 
                UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame)
            {
                TryPickup();
            }
            #else
            if (Input.GetKeyDown(pickupKey))
            {
                TryPickup();
            }
            #endif
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;

            if (autoPickup)
            {
                TryPickup();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void TryPickup()
    {
        if (isPickedUp || item == null)
            return;

        if (InventoryManager.Instance != null)
        {
            bool success = InventoryManager.Instance.AddItem(item, quantity);
            
            if (success)
            {
                isPickedUp = true;
                Debug.Log($"Recogiste: {item.itemName} x{quantity}");
                
                // Efecto visual opcional (puedes añadir animación aquí)
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualizar rango de pickup en el editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}