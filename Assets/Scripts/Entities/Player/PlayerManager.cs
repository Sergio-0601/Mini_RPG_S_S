using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    private PlayerInputHandler input;
    private PlayerMovement movement;
    private KnockbackHandler knockback;
    private PlayerCollisionDetector collisionDetector;
    private Health health;
    [Header("Respawn")]
    public Transform respawnPosition;
    void Start()
    {
        input = GetComponent<PlayerInputHandler>();
    movement = GetComponent<PlayerMovement>();
    knockback = GetComponent<KnockbackHandler>();
    collisionDetector = GetComponent<PlayerCollisionDetector>();
    health = GetComponent<Health>();
    knockback.OnStunChanged += isStunned => { movement.isStunned = isStunned; };
    collisionDetector.OnGroundedChanged += (isGrounded) => { movement.isGrounded = isGrounded; };
    health.OnDeath += Death;
    movement.isGrounded = true;
    }
    private void Update()
    {
        movement.moveInput = input.MoveInput;
        if (input.JumpPressed)
        {
            movement.Jump();
        }
    }
    public void TakeDamage(int damage, Vector2 knockbackDir, float knockbackForce, float stunDuration)
    {
        movement.moveInput = Vector2.zero;
        knockback.ApplyKnockback(knockbackDir, knockbackForce, stunDuration);
        health.TakeDamage(damage);
    }
    private void Death()
    {
        Respawn();
    }
    public void Respawn()
    {
        health.ResetHealt();
        transform.position = respawnPosition.position;
        movement.moveInput = Vector2.zero;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
    public void SetCheckpoint(Transform newCheckpoint)
    {
        respawnPosition = newCheckpoint;
    }
}
