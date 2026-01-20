using UnityEngine;
[CreateAssetMenu(fileName = "New Quest", menuName = "Quests System/Unified Quest")]
public class Quests : ScriptableObject
{
    public enum QuestType
    {
        Collect,
        Kill,
        Talk,
        Custom
    }
    public enum QuestProgress
    {
        NoStarted,
        InProgress,
        Done,
        RewardsClaimed
    }
    [Header("Quest Info")]
    public string questName;
    [TextArea] public string description;
    public QuestType questType;
    public QuestProgress questProgress = QuestProgress.NoStarted;
    [Header("Requirements to Start")]
    public Item requiredItemToStart;
    [Header("Objectives")]
    public Item itemToCollect;
    public string enemyTag;
    public int ObjectiveCount;
    public int CurrentCount;
    [Header("Rewards")]
    public int goldReward;
    public Item itemReward;
    public bool QuestStarted => questProgress != QuestProgress.NoStarted;
    public bool QuestCompleted => questProgress == QuestProgress.Done || questProgress == QuestProgress.RewardsClaimed;
    public void StartQuest()
    {
        if (questProgress == QuestProgress.NoStarted)
        {
            questProgress = QuestProgress.InProgress;
            CurrentCount = 0;
            Debug.Log($"MisiÃ³n iniciada: {questName}");
        }
    }
    public void IncrementCounter(int amount = 1)
    {
        if (questProgress != QuestProgress.InProgress) return;
        CurrentCount += amount;
        if (CurrentCount >= ObjectiveCount)
        {
            questProgress = QuestProgress.Done;
            Debug.Log($"MisiÃ³n completada: {questName}");
        }
    }
    public void CheckCompletion()
    {
        if (questType == QuestType.Collect && itemToCollect != null)
        {
            if (InventoryManager.Instance != null)
            {
                CurrentCount = InventoryManager.Instance.GetItemCount(itemToCollect);
                if (CurrentCount >= ObjectiveCount)
                {
                    questProgress = QuestProgress.Done;
                }
            }
        }
    }
    public void ResetQuest()
    {
        questProgress = QuestProgress.NoStarted;
        CurrentCount = 0;
    }
}
