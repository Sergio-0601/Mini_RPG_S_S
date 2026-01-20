using UnityEngine;
public class PlatformMovement : MonoBehaviour
{
    public float Speed = 3f;
    private SpriteRenderer sr;
    Vector2 InitialPosition = Vector2.zero;
    Vector2 FinalPosition = Vector2.zero;
    void Start()
    {
       sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        transform.position = Vector2.Lerp(InitialPosition, FinalPosition, Speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RightLimit")) 
        {
            Vector2 temp = InitialPosition;
            InitialPosition = FinalPosition;
            FinalPosition = temp;
        }
        else if (collision.CompareTag("LeftLimit")) 
        {
            Vector2 temp = InitialPosition;
            InitialPosition = FinalPosition;
            FinalPosition = temp;
        }
    }
}
