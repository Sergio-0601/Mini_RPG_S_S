using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [Header("=== DIÁLOGOS (EDITABLES EN INSPECTOR) ===")]
    [SerializeField] List<string> dialogoAntesMision;
    [SerializeField] List<string> dialogoDuranteMision;
    [SerializeField] List<string> dialogoDespuesMision;

    [Header("=== REFERENCIAS ===")]
    [SerializeField] NPCDialogueUI dialogueUI;
    [SerializeField] Quests quest;

    public void Hablar()
    {
        dialogueUI.OnFinish -= IniciarMision;
        dialogueUI.OnFinish += IniciarMision;

        if (!quest.QuestStarted)
        {
            dialogueUI.TextoVisible(dialogoAntesMision);
        }
        else if (!quest.QuestCompleted)
        {
            dialogueUI.TextoVisible(dialogoDuranteMision);
        }
        else
        {
            dialogueUI.TextoVisible(dialogoDespuesMision);
        }
    }

    void IniciarMision()
    {
        if (!quest.QuestStarted)
        {
            quest.StartQuest();
        }

        dialogueUI.OnFinish -= IniciarMision;
    }
}
