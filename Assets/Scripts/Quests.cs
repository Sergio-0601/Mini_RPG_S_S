using UnityEngine;

[CreateAssetMenu(fileName = "New Quests", menuName = "Quests System/Quests")]
public class Quests : ScriptableObject
{
    public enum QuestProgress
    {
        NoStarted,
        inProgress,
        Done
    }

    public string questName;
    [TextArea] public string description;

    public int ObjectiveCount;
    public int CurrentCount;

    public QuestProgress questProgress = QuestProgress.NoStarted;

    public void IncrementCounter()
    {
        CurrentCount++;
        if (CurrentCount >= ObjectiveCount)
        {
            questProgress = QuestProgress.Done;
        }
    }

    public void StartQuest()
    {
        if (questProgress == QuestProgress.NoStarted)
        {
            questProgress = QuestProgress.inProgress;
            CurrentCount = 0;
        }
    }

    public void Awake()
    {
        questProgress = QuestProgress.NoStarted;
        CurrentCount = 0;
    }
}
