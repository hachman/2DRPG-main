using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerQuestCompleter : MonoBehaviour
{
    public List<Quest_Data> quest_Datas;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (QuestSystemManager.instance.activeQuest == null) return;
            foreach (var item in quest_Datas)
            {
                if (QuestSystemManager.instance.activeQuest == item)
                {
                    QuestSystemManager.instance.DoneQuest(item);
                }
            }
        }
    }

}
