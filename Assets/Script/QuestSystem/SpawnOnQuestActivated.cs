using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnQuestActivated : MonoBehaviour
{
    [SerializeField] Quest_Data questActivatedRef;
    [SerializeField] List<GameObject> gameObjectsToSpawn;

    private void Start()
    {
        foreach (var item in gameObjectsToSpawn)
        {
            item.SetActive(false);
        }
        QuestSystemManager.instance.OnQuestActivated += OnQuestActivated;
        if (questActivatedRef.GetState() == Quest_State.Active) OnQuestActivated(questActivatedRef);

    }

    private void OnQuestActivated(Quest_Data data)
    {
        if (questActivatedRef == data)
        {
            foreach (var item in gameObjectsToSpawn)
            {
                item.SetActive(true);
            }
        }
    }
}
