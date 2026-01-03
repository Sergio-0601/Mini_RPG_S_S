using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("activar animación o color")]
    public SpriteRenderer sr;
    public Color activatedColor = Color.green;

    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated) return;

        if (collision.CompareTag("Player"))
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            if (player != null)
            {
                player.SetCheckpoint(transform);
                Activate();
            }
        }
    }

    private void Activate()
    {
        isActivated = true;
        if (sr != null)
            sr.color = activatedColor;
    }
}