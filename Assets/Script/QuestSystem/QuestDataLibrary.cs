using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewQuestDatabase", menuName = "Quests/Quest Database")]
public class QuestDataLibrary : ScriptableObject
{
    public List<Quest_Data> allQuests = new List<Quest_Data>();
}