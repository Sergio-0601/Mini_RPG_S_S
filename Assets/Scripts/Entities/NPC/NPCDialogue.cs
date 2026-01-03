using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] List<string> Dialogo;
    [SerializeField] NPCDialogueUI DialogueUI;
    [SerializeField] Quests Cegarro;
    public void Hablar()
    {
        DialogueUI.TextoVisible(Dialogo);
        DialogueUI.OnFinish += IniciarMisionCegarro;
        Debug.Log("Hablando con NPC");
    }

    public void IniciarMisionCegarro()
    {
        Cegarro.StartQuest();
        DialogueUI.OnFinish -= IniciarMisionCegarro;
        Debug.Log("Iniciar mision");
    }

}
