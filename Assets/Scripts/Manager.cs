using System;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    [SerializeField] public NPCDialogueUI UserInterface;
    public GameObject[] Botones = new GameObject[3];
    public event Action<int> OnChoose;
    private int possibleChoices = 0;

    private DialogueNode currentnode;

    public void Awake()
    {
        Instance = this;
    }
    public void PintarTexto(DialogueNode node)
    {
        DrawButtons(false);
        currentnode = node;
        possibleChoices = node.NextNodes.Count();
        UserInterface.OnFinish += Finishletter;
        UserInterface.TextoVisible(node.Dialogo);
        for (int i = 0; i < possibleChoices; i++) 
        {
            Botones[i].GetComponentInChildren<TMPro.TMP_Text>().text = node.buttons[i];
        }
    }
    public void OnCLickButton(GameObject ButtonPress)
    {
        int Eleccion = 0;
        foreach (GameObject Boton in Botones) 
        {
            if(Boton == ButtonPress)
            {
                currentnode.Choose(Eleccion);
                return;
            }
            Eleccion++;
        }
    }
    public void Finishletter()
    {
        
        for (int i = 0; i < possibleChoices; i++) 
        {
            Botones[i].SetActive(true);
        }
        UserInterface.OnFinish -= Finishletter; 
    }

    private void DrawButtons(bool Active)
    {
        for (int i = 0; i < Botones.Length; i++) 
        {
            Botones[i].SetActive(Active);
        }
    }
    public void Next()
    {
        if (UserInterface.Next())
        {
            currentnode.StartMision();
        }
        


    }
    public bool IsInConversation()
    {
        return UserInterface.IsInConversation();
    }


}
