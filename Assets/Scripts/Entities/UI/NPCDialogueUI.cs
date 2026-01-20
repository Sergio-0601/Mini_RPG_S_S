using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class NPCDialogueUI : MonoBehaviour

{
    [SerializeField] GameObject Textbox;
    [SerializeField] TMP_Text Texto;
    [SerializeField] float Timeletras;
    List<string> dialogos;
    int Iterador = 0;

    public event Action OnFinish;
    public void TextoVisible(List<string> textovisible)
    {
        Iterador = 0;
        dialogos = textovisible;
        StartCoroutine(TextAppear(dialogos[Iterador], Texto, Timeletras));
        Textbox.SetActive(true);
    }

    public bool Next()
    {
        if (Texto.text == dialogos[Iterador])
        {
            Iterador++;
            if (Iterador < dialogos.Count)
            {

                StartCoroutine(TextAppear(dialogos[Iterador], Texto, Timeletras));

            }
            else
            {
                Textbox.SetActive(false);
                OnFinish?.Invoke();
            }
            return true;
        }
        else
        {
            StopAllCoroutines();
            Texto.text = dialogos[Iterador];
            OnFinish?.Invoke();
            return false;
        }

    }
    IEnumerator<WaitForSeconds> TextAppear(string textovisible, TMP_Text letravisible, float tiempo)
    {
        letravisible.text = "";
        foreach (char letra in textovisible)
        {
            letravisible.text += letra;
            yield return new WaitForSeconds(tiempo);
        }
        OnFinish?.Invoke();
    }

    public bool IsInConversation()
    {
        return Textbox.activeSelf;
    }
}
