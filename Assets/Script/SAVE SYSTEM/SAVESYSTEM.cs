using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class SAVESYSTEM : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject savedPanel;
    private void Start()
    {
        savedPanel?.SetActive(false);
    }
    public void SaveProgress()
    {
        QuestSystemManager.instance?.SaveQuestData(playerTransform.position, SceneManager.GetActiveScene().name);
        StartCoroutine(DisplaySavedPAnel());
    }
    IEnumerator DisplaySavedPAnel()
    {
        savedPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        savedPanel.SetActive(false);
    }

   

}
