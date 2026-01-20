using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueSystem")]
public class DialogueNode : ScriptableObject
{
    [SerializeField] public List<string> Dialogo;
    [SerializeField] public List<string> buttons;
    [SerializeField] public Quests MisionActivar;
    public List<DialogueNode> NextNodes;

    public void PintarTexto()
    {

        Manager.Instance.PintarTexto(this);
    }
    public void Choose(int Decision)
    {
        NextNodes[Decision].PintarTexto();
        StartMision();
    }

    public void StartMision()
    {
        if (null != MisionActivar)
        {
            MisionActivar.StartQuest();
        }
    }




}