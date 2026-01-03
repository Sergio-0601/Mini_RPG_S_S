using UnityEngine;

public class GroundEnemyManager : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float maxSpeed = 5f;
    public bool facingRight = true;

    [Header("Daño al Player")]
    public int damage = 1;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.3f;

    [HideInInspector] public Rigidbody2D rb;
    private Health health;
    private KnockbackHandler knockback;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        knockback = GetComponent<KnockbackHandler>();

        health.OnDeath += OnDie;
    }

    public void TakeDamage(int damage, Vector2 knockbackDir, float knockbackForce, float stunDuration)
    {
        if (knockback) 
            knockback.ApplyKnockback(knockbackDir, knockbackForce, knockbackDuration);

        health.TakeDamage(damage);
    }

    private void OnDie()
    {
         Destroy(gameObject);
    }
}