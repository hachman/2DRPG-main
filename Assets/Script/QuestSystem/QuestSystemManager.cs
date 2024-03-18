using System;
using System.Collections.Generic;
// using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class QuestSystemManager : MonoBehaviour
{
    public Action<Quest_Data> OnQuestActivated;
    public Action<Quest_Data> OnQuestDone;


    public Quest_Data activeQuest { get; set; }
    string filePath;  // Fixed file path concatenation

    [SerializeField] List<Quest_Data> allQuests;


    public QuestDatabaseJSON questDatabaseJSON = new(); // Renamed allQuests to questDatabase
    public bool RepositionPlayer = false;
    public static QuestSystemManager instance;
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



            LoadQuestData((databaseJSONCallback) =>
            {
                questDatabaseJSON = databaseJSONCallback;

                foreach (var item in allQuests)
                {
                    QuestDataJSON questdata = questDatabaseJSON.allquestsData.Find(data => data.questName == item.questName);
                    item.SetData(questdata);
                    if (item.questName == questDatabaseJSON.ActiveQuestName)
                    {
                        activeQuest = item;
                    }
                }


            });
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public void SaveQuestData() // this is just temporary
    {
        List<QuestDataJSON> jsonQuestsData = new();
        foreach (var item in allQuests)
        {
            jsonQuestsData.Add(item.GetJsonData());
        }
        questDatabaseJSON.allquestsData = jsonQuestsData;

        string jsonData = JsonUtility.ToJson(questDatabaseJSON); // Fixed variable name
        System.IO.File.WriteAllText(filePath, jsonData);


        Debug.Log("Saved");
    }
    public void SaveQuestData(Vector2 playerPos, string sceneName)
    {

        List<QuestDataJSON> jsonQuestsData = new();
        foreach (var item in allQuests)
        {
            jsonQuestsData.Add(item.GetJsonData());
        }
        questDatabaseJSON.allquestsData = jsonQuestsData;
        questDatabaseJSON.xSavedPosition = playerPos.x;
        questDatabaseJSON.ySavedPosition = playerPos.y;
        questDatabaseJSON.savedScene = sceneName;
        questDatabaseJSON.ActiveQuestName = activeQuest?.questName;


        string jsonData = JsonUtility.ToJson(questDatabaseJSON); // Fixed variable name
        System.IO.File.WriteAllText(filePath, jsonData);


        Debug.Log("Saved");
    }

    public void LoadQuestData(Action<QuestDatabaseJSON> loadCallback)
    {
        if (System.IO.File.Exists(filePath))
        {
            QuestDatabaseJSON _questDatabaseJSON = new();
            string jsonData = System.IO.File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(jsonData))
            {
                JsonUtility.FromJsonOverwrite(jsonData, _questDatabaseJSON); // Fixed variable name
                loadCallback?.Invoke(_questDatabaseJSON);
            }
            else
            {
                Debug.LogWarning("Quest data file is empty: " + filePath);

            }
        }
        else
        {
            Debug.LogWarning("Quest data file not found at path: " + filePath);

        }
    }



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
    public List<QuestDataJSON> allquestsData = new();
    public List<string> doneQuests = new();

}
#if UNITY_EDITOR
[CustomEditor(typeof(QuestSystemManager))]
public class QuestSystemManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        QuestSystemManager manager = (QuestSystemManager)target;

        EditorGUILayout.Space();

        if (GUILayout.Button("Save Quests Data"))
        {
            // Call the ActivateQuest method of the QuestSystemManager
            manager.SaveQuestData();
        }
    }
}
#endif