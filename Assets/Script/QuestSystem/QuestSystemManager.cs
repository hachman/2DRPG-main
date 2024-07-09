 using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using System.IO;
// using NUnit.Framework;

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestSystemManager : MonoBehaviour
{
    public Action<Quest_Data> OnQuestActivated;
    public Action<Quest_Data> OnQuestDone;


    public Quest_Data activeQuest { get; set; }
    string filePath;  // Fixed file path concatenation

    [SerializeField] List<Quest_Data> allQuests;


    public QuestDatabaseJSON questDatabaseJSON = new(); // Renamed allQuests to questDatabase
    public bool RepositionPlayer = false;
    public static QuestSystemManager instance = null;
    public void ResetQuests()
    {
        activeQuest = null;
        foreach (var item in allQuests)
        {
            item.ResetData();
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            filePath = Application.persistentDataPath + "/questdtb.json";
            Debug.Log(filePath);
            ResetQuests();
            // LoadQuestData((databaseJSONCallback) =>
            // {
            //     questDatabaseJSON = databaseJSONCallback;

            //     foreach (var item in allQuests)
            //     {
            //         QuestDataJSON questdata = questDatabaseJSON.allquestsData.Find(data => data.questName == item.questName);
            //         item.SetData(questdata);
            //         if (item.questName == questDatabaseJSON.ActiveQuestName)
            //         {
            //             activeQuest = item;
            //         }
            //     }


            // });
        }
        else
        {
            Destroy(gameObject);
        }


    }
    public void LoadData(Action<QuestDatabaseJSON> playerPosLoadCB)
    {
        if (!File.Exists(filePath))
        {
            playerPosLoadCB?.Invoke(new QuestDatabaseJSON());
            return;
        }

        string jsonData = File.ReadAllText(filePath);
        QuestDatabaseJSON _questDatabaseJSON = JsonUtility.FromJson<QuestDatabaseJSON>(jsonData);
        playerPosLoadCB?.Invoke(_questDatabaseJSON);
        // delay then infoke

    }
    public void SetQuestSystem(QuestDatabaseJSON _questDatabaseJSON)
    {
        ResetQuests();
        if (!string.IsNullOrEmpty(_questDatabaseJSON.ActiveQuestName))
        {
            Debug.Log("ACtive Quest Exists");
            activeQuest = allQuests.Find(data => data.questName == _questDatabaseJSON.ActiveQuestName);
            activeQuest?.SetState(Quest_State.Active);
        }
        if (_questDatabaseJSON.doneQuests.Count > 0)
        {
            foreach (var item in _questDatabaseJSON.doneQuests)
            {
                Debug.Log("Done Quest Exists");
                var Data = allQuests.Find(data => data.questName == item);
                // Debug.Log(string.Format($"Settings State Done on Quest {Data?.questName}"));
                Data?.SetState(Quest_State.Done);
            };
        }
    }



    [Button]
    public void SampleSaveData() => SaveQuestData(new Vector2(1, 1), SceneManager.GetActiveScene().name);




    public void SaveQuestData(Vector2 playerPos, string sceneName)
    {


        // list of finished quests
        List<string> doneQuestString = new();
        foreach (var item in allQuests)
        {
            if (item.GetState() == Quest_State.Done) doneQuestString.Add(item.questName);
        }


        QuestDatabaseJSON _questDatabaseJSON = new();
        // _questDatabaseJSON.allquestsData.AddRange(allQuestsString);
        _questDatabaseJSON.xSavedPosition = playerPos.x;
        _questDatabaseJSON.ySavedPosition = playerPos.y;
        _questDatabaseJSON.savedScene = sceneName;
        _questDatabaseJSON.ActiveQuestName = activeQuest?.questName;
        _questDatabaseJSON.doneQuests.AddRange(doneQuestString);
        Debug.Log("Saved Data: " + filePath);

        string jsonData = JsonUtility.ToJson(_questDatabaseJSON); // Fixed variable name
        System.IO.File.WriteAllText(filePath, jsonData);


        Debug.Log("Saved");
    }

    // public void LoadQuestData(Action<QuestDatabaseJSON> loadCallback)
    // {
    //     if (System.IO.File.Exists(filePath))
    //     {
    //         QuestDatabaseJSON _questDatabaseJSON = new();
    //         string jsonData = System.IO.File.ReadAllText(filePath);
    //         if (!string.IsNullOrEmpty(jsonData))
    //         {
    //             JsonUtility.FromJsonOverwrite(jsonData, _questDatabaseJSON); // Fixed variable name
    //             loadCallback?.Invoke(_questDatabaseJSON);
    //         }
    //         else
    //         {
    //             Debug.LogWarning("Quest data file is empty: " + filePath);

    //         }
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Quest data file not found at path: " + filePath);

    //     }
    // }



    public void ActivateQuest(Quest_Data questData)
    {
        // Add activation logic here
        questData.questState = Quest_State.Active;

        activeQuest = questData;

        OnQuestActivated?.Invoke(questData);
    }
    public void DoneQuest(Quest_Data questData)
    {
        Debug.Log("Done Quest: " + questData.questName);

        questData.questState = Quest_State.Done;
        questDatabaseJSON.doneQuests.Add(questData.questName);

        OnQuestDone?.Invoke(questData);
        activeQuest = null;
    }
}

[Serializable]
public class QuestDatabaseJSON
{

    public string ActiveQuestName;
    public float xSavedPosition, ySavedPosition;
    public string savedScene;
    // public List<string> allquestsData = new();
    public List<string> doneQuests = new();

}
#if UNITY_EDITOR
// [CustomEditor(typeof(QuestSystemManager))]
// public class QuestSystemManagerEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();

//         QuestSystemManager manager = (QuestSystemManager)target;

//         EditorGUILayout.Space();

//         if (GUILayout.Button("Save Quests Data"))
//         {
//             // Call the ActivateQuest method of the QuestSystemManager
//             manager.SaveQuestData();
//         }
//     }
// }
#endif
/*
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestSystemManager : MonoBehaviour
{
    public static QuestSystemManager instance { get; private set; }

    public event Action<Quest_Data> OnQuestActivated;
    public event Action<Quest_Data> OnQuestDone;

    public Quest_Data activeQuest { get; private set; }

    [SerializeField] private List<Quest_Data> allQuests;
    private string filePath;

    public bool RepositionPlayer = false;
    public QuestDatabaseJSON questDatabaseJSON = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            filePath = Path.Combine(Application.persistentDataPath, "questdtb.json");
            Debug.Log(filePath);
            ResetQuests();
            LoadData(questData => SetQuestSystem(questData));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetQuests()
    {
        activeQuest = null;
        foreach (var quest in allQuests)
        {
            quest.ResetData();
        }
    }

    public void LoadData(Action<QuestDatabaseJSON> callback)
    {
        if (!File.Exists(filePath))
        {
            callback?.Invoke(new QuestDatabaseJSON());
            return;
        }

        try
        {
            string jsonData = File.ReadAllText(filePath);
            QuestDatabaseJSON questData = JsonUtility.FromJson<QuestDatabaseJSON>(jsonData);
            callback?.Invoke(questData);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error loading data: {ex.Message}");
            callback?.Invoke(new QuestDatabaseJSON());
        }
    }

    public void SetQuestSystem(QuestDatabaseJSON questData)
    {
        ResetQuests();

        if (!string.IsNullOrEmpty(questData.ActiveQuestName))
        {
            Debug.Log("Active Quest Exists");
            activeQuest = allQuests.Find(q => q.questName == questData.ActiveQuestName);
            activeQuest?.SetState(Quest_State.Active);
        }

        foreach (var questName in questData.doneQuests)
        {
            Debug.Log("Done Quest Exists");
            var quest = allQuests.Find(q => q.questName == questName);
            quest?.SetState(Quest_State.Done);
        }
    }

    [NaughtyAttributes.Button]
    public void SampleSaveData() => SaveQuestData(new Vector2(1, 1), SceneManager.GetActiveScene().name);

    public void SaveQuestData(Vector2 playerPos, string sceneName)
    {
        List<string> doneQuests = new();
        foreach (var quest in allQuests)
        {
            if (quest.GetState() == Quest_State.Done)
            {
                doneQuests.Add(quest.questName);
            }
        }

        QuestDatabaseJSON questData = new()
        {
            xSavedPosition = playerPos.x,
            ySavedPosition = playerPos.y,
            savedScene = sceneName,
            ActiveQuestName = activeQuest?.questName,
            doneQuests = doneQuests
        };

        try
        {
            string jsonData = JsonUtility.ToJson(questData);
            File.WriteAllText(filePath, jsonData);
            Debug.Log("Saved Data: " + filePath);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error saving data: {ex.Message}");
        }
    }

    public void ActivateQuest(Quest_Data quest)
    {
        quest.SetState(Quest_State.Active);
        activeQuest = quest;
        OnQuestActivated?.Invoke(quest);
    }

    public void CompleteQuest(Quest_Data quest)
    {
        Debug.Log("Completed Quest: " + quest.questName);
        quest.SetState(Quest_State.Done);
        questDatabaseJSON.doneQuests.Add(quest.questName);
        OnQuestDone?.Invoke(quest);
        activeQuest = null;
    }
}

[Serializable]
public class QuestDatabaseJSON
{
    public string ActiveQuestName;
    public float xSavedPosition, ySavedPosition;
    public string savedScene;
    public List<string> doneQuests = new();
}
*/