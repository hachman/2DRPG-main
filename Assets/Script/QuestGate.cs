using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGate : MonoBehaviour
{
    public Quest_Data completedQuestToOpen;

    private void Awake()
    {
        QuestSystemManager.instance.OnQuestDone += OnQuestDoneCheck;

    }
    private void OnQuestDoneCheck(Quest_Data questData)
    {
        if (questData == completedQuestToOpen)
        {
            gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        if (completedQuestToOpen.questState == Quest_State.Done) gameObject.SetActive(false);

    }
}
