using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f;

    private Rigidbody2D rb;
    private Animator animator; // Para animaciones después (opcional)
    private Vector2 lastDirection = Vector2.down; // Dirección que mira

    public Vector2 moveInput { get; set; }
    public bool isGrounded { get; set; } // Ya no se usa pero lo dejamos para no romper código
    public bool isStunned { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Opcional
        
        if (rb == null)
        {
            Debug.LogError("PlayerMovement: No se encontró Rigidbody2D");
        }
        else
        {
            // Configurar para movimiento top-down
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
        // Movimiento en 4 direcciones (o 8 si usas diagonal)
        Vector2 movement = moveInput.normalized * velocidad;
        
        // Aplicar movimiento
        rb.linearVelocity = movement;

        // Guardar la última dirección para animaciones/flip
        if (moveInput.magnitude > 0.1f)
        {
            lastDirection = moveInput.normalized;
            UpdateDirection();
        }
    }

    private void UpdateDirection()
    {
        // Flip horizontal según la dirección
        if (moveInput.x > 0.1f)
        {
            // Mirando derecha
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < -0.1f)
        {
            // Mirando izquierda
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Si tienes Animator, puedes añadir parámetros aquí
        if (animator != null)
        {
            animator.SetFloat("Horizontal", lastDirection.x);
            animator.SetFloat("Vertical", lastDirection.y);
            animator.SetFloat("Speed", moveInput.magnitude);
        }
    }

    public void Jump()
    {
        // En top-down no hay salto, pero lo dejamos para no romper PlayerManager
        // Puedes usarlo para otra mecánica como "dash" o "interactuar"
    }

    public Vector2 GetLastDirection()
    {
        return lastDirection;
    }
}