using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocidad = 8f;
    [SerializeField] private float velocidadMaxima = 8f;
    [SerializeField] private float salto = 12f;

    private Rigidbody2D rb;

    public Vector2 moveInput { get; set; }    // Input que viene del PlayerCore
    public bool isGrounded { get; set; }       // Para controlar si puede saltar
    public bool isStunned { get; set; }        // Para controlar si puede moverse

    private void Awake()
    {
       
       
    }

    private void Update()
    {
        if (isStunned)
        {
            return;
        }
        // Aplicar fuerza horizontal solo si no excede la velocidad máxima
        transform.position += new Vector3 (moveInput.x, moveInput.y, 0).normalized *0.01f;
    }

    public void Jump()
    {
        if (isGrounded && !isStunned)
        {
            rb.AddForce(Vector2.up * salto, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
}
