using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocidad = 8f;
    [SerializeField] private float salto = 12f;

    private Rigidbody2D rb;
    private bool facingRight = true;

    public Vector2 moveInput { get; set; }
    public bool isGrounded { get; set; }
    public bool isStunned { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            Debug.LogError("PlayerMovement: No se encontró Rigidbody2D");
        }
        else
        {
            
            rb.gravityScale = 3f;
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
        
        float horizontalInput = moveInput.x;
        
        
        rb.linearVelocity = new Vector2(horizontalInput * velocidad, rb.linearVelocity.y);

        
        if (horizontalInput > 0.1f && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < -0.1f && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Jump()
    {
        if (rb == null) return;
        
        if (isGrounded && !isStunned)
        {
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            
            
            rb.AddForce(Vector2.up * salto, ForceMode2D.Impulse);
        }
    }
}