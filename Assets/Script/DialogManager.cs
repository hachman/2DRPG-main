using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    private Dialog currentDialog;
    private int currentLine = 0;
    private bool isTyping;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start()
    {
        dialogBox.SetActive(false);
    }

    public static DialogManager Instance { get; private set; }
    Action DialogDone;
    public void ShowDialog(Dialog dialog, Action onDialogDone)
    {

        if (dialog.Lines.Count <= 0) return;
        if (isTyping) // Avoid showing new dialog while typing
            return;
        DialogDone = onDialogDone;
        currentDialog = dialog;
        dialogBox.SetActive(true);
        OnShowDialog?.Invoke();
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void NextLine()
    {
        if (isTyping) return;
        currentLine++;
        if (currentLine < currentDialog.Lines.Count)
        {
            StartCoroutine(TypeDialog(currentDialog.Lines[currentLine]));
        }
        else
        {
            dialogBox.SetActive(false);
            currentLine = 0;
            OnHideDialog?.Invoke();
            DialogDone?.Invoke();
        }

    }

    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (char letter in line)
        {
            dialogText.text += letter;
            AudioManager.instance.PlaySoundEffect("talk");
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }
}
