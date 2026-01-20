using UnityEngine;
public class CegarroScript : MonoBehaviour
{
    [SerializeField] Quests CegarroQuest;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && CegarroQuest.questProgress == Quests.QuestProgress.InProgress ) 
        {
           CegarroQuest.IncrementCounter();
           Destroy(gameObject);
        }
    }
    void Start()
    {
    }
    void Update()
    {
    }
}
