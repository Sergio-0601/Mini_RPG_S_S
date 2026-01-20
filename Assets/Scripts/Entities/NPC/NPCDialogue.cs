using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [Header("=== DIALOGOS (REQUISITO NO CUMPLIDO) ===")]
    [SerializeField] List<string> dialogoSinRequisito;
    [Header("=== DIALOGOS (PARA EMPEZAR) ===")]
    [SerializeField] List<string> dialogoEmpezarMision;
    [Header("=== DIALOGOS (DURANTE MISION) ===")]
    [SerializeField] List<string> dialogoDuranteMision;
    [Header("=== DIALOGOS (COMPLETADA / RECOMPENSA) ===")]
    [SerializeField] List<string> dialogoMisionLista;
    [SerializeField] List<string> dialogoPostMision1;
    [SerializeField] List<string> dialogoPostMision2;
    [Header("=== REFERENCIAS ===")]
    [SerializeField] NPCDialogueUI dialogueUI;
    [SerializeField] Quests quest;
    private bool flipFlopAfter = false;
    public void Hablar()
    {
        if (dialogueUI == null) dialogueUI = Object.FindFirstObjectByType<NPCDialogueUI>();
        if (dialogueUI == null || quest == null)
        {
            Debug.LogError("NPCDialogue: dialogueUI o quest no est asignado.");
            return;
        }
        dialogueUI.OnFinish -= LogicaTrasDialogue;
        dialogueUI.OnFinish += LogicaTrasDialogue;
        if (quest.requiredItemToStart != null && !InventoryManager.Instance.HasItem(quest.requiredItemToStart))
        {
            dialogueUI.TextoVisible(dialogoSinRequisito);
            return;
        }
        switch (quest.questProgress)
        {
            case Quests.QuestProgress.NoStarted:
                dialogueUI.TextoVisible(dialogoEmpezarMision);
                break;
            case Quests.QuestProgress.InProgress:
                quest.CheckCompletion();
                if (quest.QuestCompleted)
                    dialogueUI.TextoVisible(dialogoMisionLista);
                else
                    dialogueUI.TextoVisible(dialogoDuranteMision);
                break;
            case Quests.QuestProgress.Done:
                dialogueUI.TextoVisible(dialogoMisionLista);
                break;
            case Quests.QuestProgress.RewardsClaimed:
                if (flipFlopAfter && dialogoPostMision2 != null && dialogoPostMision2.Count > 0)
                    dialogueUI.TextoVisible(dialogoPostMision2);
                else
                    dialogueUI.TextoVisible(dialogoPostMision1);
                flipFlopAfter = !flipFlopAfter;
                break;
        }
    }
    void LogicaTrasDialogue()
    {
        dialogueUI.OnFinish -= LogicaTrasDialogue;
        if (quest.questProgress == Quests.QuestProgress.NoStarted)
        {
            quest.StartQuest();
        }
        else if (quest.questProgress == Quests.QuestProgress.Done)
        {
            EntregarRecompensas();
        }
    }
    void EntregarRecompensas()
    {
        Debug.Log("Entregando recompensas...");
        if (quest.questType == Quests.QuestType.Collect && quest.itemToCollect != null)
        {
            InventoryManager.Instance.RemoveItem(quest.itemToCollect, quest.ObjectiveCount);
        }
        if (quest.itemReward != null)
        {
            InventoryManager.Instance.AddItem(quest.itemReward);
        }
        quest.questProgress = Quests.QuestProgress.RewardsClaimed;
    }
}
