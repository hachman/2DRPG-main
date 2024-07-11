using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreSaveLoad : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    private void Start()
    {
        QuestSystemManager.instance?.LoadData((loadedData) =>
        {
            if (string.IsNullOrEmpty(loadedData.savedScene))
            {
                Debug.Log("PreSaveLoad Initialized");
                QuestSystemManager.instance?.SaveQuestData(playerTransform.position, SceneManager.GetActiveScene().name);
            }
        });
    }
}


