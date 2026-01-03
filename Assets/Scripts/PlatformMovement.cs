using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float Speed = 3f;
    private int Direction = 1;
    private SpriteRenderer sr;
    private float Aceleration = 3;

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
            InitialPosition = FinalPosition;
            FinalPosition = InitialPosition;
        }
        else if (collision.CompareTag("LeftLimit")) 
        {
            InitialPosition = FinalPosition;
            FinalPosition = InitialPosition;
        }
    }
}
