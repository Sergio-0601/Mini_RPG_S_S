using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Quests;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] DialogueNode StartConv;
    [SerializeField] DialogueNode InProgressConv;
    [SerializeField] DialogueNode EndConv;
    [SerializeField] Quests Cegarro;
    public void Hablar()
    {

        if (Manager.Instance.IsInConversation())
        {
            return;
        }


        if (QuestProgress.inProgress == Cegarro.questProgress)
        {
            InProgressConv.PintarTexto();

        }
        else if (QuestProgress.Done == Cegarro.questProgress)
        {
            EndConv.PintarTexto();
        }
        else
        {
            StartConv.PintarTexto();
            //DialogueUI.OnFinish += IniciarMisionCegarro;
            Debug.Log("Hablando con NPC");

        }

    }

    public void IniciarMisionCegarro()
    {
        Cegarro.StartQuest();
        //DialogueUI.OnFinish -= IniciarMisionCegarro;
        Debug.Log("Iniciar mision");
    }

}