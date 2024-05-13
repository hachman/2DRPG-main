using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveQuestTrigger : MonoBehaviour
{
    [SerializeField] Quest_Data questReference;

    [SerializeField] UnityEvent OnQuestDone;

    private void Start()
    {
        if (!questReference) return;
        if (questReference.GetState() == Quest_State.Done || questReference.GetState() == Quest_State.Active) OnQuestDone?.Invoke();
        QuestSystemManager.instance.OnQuestActivated += (quest) => { if (quest == questReference) OnQuestDone?.Invoke(); };

    }

}
