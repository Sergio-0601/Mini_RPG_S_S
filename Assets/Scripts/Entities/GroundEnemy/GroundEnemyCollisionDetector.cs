using UnityEngine;

public class GroundEnemyCollisionDetector : MonoBehaviour
{
    private GroundEnemyManager core;

    private void Awake()
    {
        core = GetComponent<GroundEnemyManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            float xDir = Mathf.Sign(collision.transform.position.x - transform.position.x);
            Vector2 knockDir = new Vector2(xDir, 1f);
            if (collision.contacts[0].normal.y > -0.5)
            {
                PlayerManager pm = collision.collider.GetComponent<PlayerManager>();
                if (pm != null)
                    pm.TakeDamage(core.damage, knockDir, core.knockbackForce, core.knockbackDuration);
            }
            else
            {
                   core.TakeDamage(1, Vector2.zero, core.knockbackForce, core.knockbackDuration);
            }
        }
    }
}
