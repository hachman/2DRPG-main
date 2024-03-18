using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayBackgroundMusic("menu");
    }
    public void PlayGame()
    {
        AudioManager.instance.PlaySoundEffect("click");
        SceneManager.LoadSceneAsync(1);
        QuestSystemManager.instance.ResetQuests();
    }
    public void OptionMenu()
    {
        
    }
    public void LoadGame()
    {
        AudioManager.instance.PlaySoundEffect("click");
        if (string.IsNullOrEmpty(QuestSystemManager.instance.questDatabaseJSON.ActiveQuestName)) return;
        QuestSystemManager.instance.RepositionPlayer = true;
        SceneManager.LoadScene(QuestSystemManager.instance.questDatabaseJSON.savedScene);
    }
    public void QuitGame()
    {
        AudioManager.instance.PlaySoundEffect("click");
        Application.Quit();
    }
    public void OnNewGameClicked()
    {
        AudioManager.instance.PlaySoundEffect("click");
        DataPersistenceManager.instance.NewGame();
    }
    public void OnLoadGameClicked()
    {
        AudioManager.instance.PlaySoundEffect("click");
        DataPersistenceManager.instance.LoadGame();
    }
    public void OnSaveGameClicked()
    {
        AudioManager.instance.PlaySoundEffect("click");
        DataPersistenceManager.instance.SaveGame();
    }
}
