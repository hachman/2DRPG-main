using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "New Kill Quest", menuName = "Quests/Kill Quest")]

public class KillQuest : Quest_Data
{
    public int MaxCount;
    public int currentCount;
    public void SetCurrentCount(int count)
    {
        currentCount = count;
    }
    public void Increment()
    {
        currentCount++;
        if (currentCount >= MaxCount)
        {
            QuestSystemManager.instance.DoneQuest(this);
        }
    }
    public override void Progress()
    {
        base.Progress();
        Increment();
    }
    public override void ResetData()
    {
        base.ResetData();

        currentCount = 0;
    }
    public override QuestDataJSON GetJsonData()
    {
        // base.GetJsonData();
        List<string> reqNames = new();
        foreach (var item in requirements)
        {
            reqNames.Add(item.questName);
        }

        QuestDataJSON jsonData = new()
        {
            questDefinition = questDefinition,
            requirementNames = reqNames,
            questName = questName,
            questState = questState,
            currentKills = currentCount,
            maxKills = MaxCount

        };
        return jsonData;

    }
    public override void SetData(QuestDataJSON questDataJSON)
    {

        questState = questDataJSON.questState;
        currentCount = questDataJSON.currentKills;
        // MaxCount = questDataJSON.maxKills;
    }
}
