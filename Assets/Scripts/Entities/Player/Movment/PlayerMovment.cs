using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 lastDirection = Vector2.down;
    public Vector2 moveInput { get; set; }
    public bool isGrounded { get; set; }
    public bool isStunned { get; set; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (rb == null)
        {
            Debug.LogError("PlayerMovement: No se encontró Rigidbody2D");
        }
        else
        {
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private void FixedUpdate()
    {
        if (isStunned || rb == null)
        {
            return;
        }
        Move();
    }
    private void Move()
    {
        Vector2 movement = moveInput.normalized * velocidad;
        rb.linearVelocity = movement;
        if (moveInput.magnitude > 0.1f)
        {
            lastDirection = moveInput.normalized;
            UpdateDirection();
        }
    }
    private void UpdateDirection()
    {
        if (moveInput.x > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (animator != null)
        {
            animator.SetFloat("Horizontal", lastDirection.x);
            animator.SetFloat("Vertical", lastDirection.y);
            animator.SetFloat("Speed", moveInput.magnitude);
        }
    }
    public void Jump()
    {
    }
    public Vector2 GetLastDirection()
    {
        return lastDirection;
    }
}
