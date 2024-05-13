using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerQuestHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] GameObject questCompleted;
    [SerializeField] TextMeshProUGUI questCompletedText;
    [SerializeField] Transform playerUnit;
    // [SerializeField] GameObject savedPanel;
    private void Awake()
    {
        QuestSystemManager.instance.OnQuestActivated += OnQuestActivated;
        QuestSystemManager.instance.OnQuestDone += OnQuestDone;
    }


    // public void SaveData()
    // {

    //     QuestSystemManager.instance.SaveQuestData(playerUnit.position, SceneManager.GetActiveScene().name);
    //     StartCoroutine(DisplaySavedPAnel());
    // }
    // IEnumerator DisplaySavedPAnel()
    // {
    //     savedPanel.SetActive(true);
    //     yield return new WaitForSeconds(1f);
    //     savedPanel.SetActive(false);
    // }

    private void OnQuestDone(Quest_Data data)
    {
        StartCoroutine(DisplayCompleted(data.questDefinition));
    }
    IEnumerator DisplayCompleted(string questDef)
    {
        questCompleted.SetActive(true);
        questCompletedText.text = questDef;
        textMeshProUGUI.text = string.Empty;
        yield return new WaitForSeconds(1f);
        questCompleted.SetActive(false);
        questCompletedText.text = string.Empty;
    }
    private void OnDisable()
    {
        QuestSystemManager.instance.OnQuestActivated -= OnQuestActivated;
        QuestSystemManager.instance.OnQuestDone -= OnQuestDone;
    }

    private void OnQuestActivated(Quest_Data data)
    {
        textMeshProUGUI.text = data.questDefinition;

    }

    private void Start()
    {
        if (QuestSystemManager.instance.RepositionPlayer)
        {
            playerUnit.position = new Vector2(QuestSystemManager.instance.questDatabaseJSON.xSavedPosition, QuestSystemManager.instance.questDatabaseJSON.ySavedPosition);
            QuestSystemManager.instance.RepositionPlayer = false;
        }
        questCompleted.SetActive(false);
        // savedPanel.SetActive(false);
        if (QuestSystemManager.instance.activeQuest != null) textMeshProUGUI.text = QuestSystemManager.instance.activeQuest.questDefinition;
    }
}