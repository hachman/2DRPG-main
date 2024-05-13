using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[Serializable]
[CreateAssetMenu(fileName = "New Quest Data", menuName = "Quests/Quest Data")]
public class Quest_Data : ScriptableObject
{


    public string questName;
    public Quest_State questState = Quest_State.Inactive;
    public string questDefinition;
    public List<Quest_Data> requirements = new();
    public bool IsRequirentsDone()
    {
        if (requirements.Count <= 0) return true;
        foreach (var item in requirements)
        {
            if (item.questState != Quest_State.Done)
            {
                return false;
            }

        }
        return true;

    }
    public virtual void Progress() { }
    public virtual void ResetData()
    {
        questState = Quest_State.Inactive;
    }
    public virtual QuestDataJSON GetJsonData()
    {
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
            questState = questState

        };
        return jsonData;
    }
    public virtual void SetData(QuestDataJSON questDataJSON)
    {
        questState = questDataJSON.questState;
    }
    public virtual void SetState(Quest_State setState)
    {
        questState = setState;
    }
    public Quest_State GetState() => questState;

}
[Serializable]
public class QuestDataJSON
{
    public string questName;
    public List<string> requirementNames = new();
    public string questDefinition;
    public Quest_State questState;
    public int currentKills;
    public int maxKills;
}
public enum Quest_State
{
    Inactive,
    Active,
    Done
}