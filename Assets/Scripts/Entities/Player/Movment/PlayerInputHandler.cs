using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControl controls;

    public Vector2 MoveInput { get; set; }
    public bool JumpPressed { get; set; }
    private GameObject NpcCheck = null;
    private void Awake()
    {
        controls = new PlayerControl();
    }

    private void OnEnable()
    {
        controls.Player.Enable();

        // Movimiento
        controls.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += _ => MoveInput = Vector2.zero;

        // Salto
        controls.Player.Jump.performed += _ => JumpPressed = true;
        // Interactuar
        controls.Player.Interact.performed += _ => Interact();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void LateUpdate()
    {
        JumpPressed = false;
    }
    public void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            NpcCheck = collision.gameObject;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            NpcCheck = null;
        }
    }
    private void  Interact() 
    {
        if (NpcCheck)
        {
            NpcCheck.GetComponent<NPCDialogue>().Hablar();
        }
        
    }

}
