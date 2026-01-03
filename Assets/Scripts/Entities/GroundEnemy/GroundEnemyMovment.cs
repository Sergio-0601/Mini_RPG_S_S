using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GroundEnemyManager core;

    private void Awake()
    {
        core = GetComponent<GroundEnemyManager>();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(core.rb.linearVelocity.x) < core.maxSpeed)
        {
            float dir = core.facingRight ? 1f : -1f;
            core.rb.AddForce(Vector2.right * dir * core.speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Borde"))
            core.facingRight = !core.facingRight;
    }
}