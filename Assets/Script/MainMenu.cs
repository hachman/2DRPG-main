using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void OptionMenu()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OnNewGameClicked()
    {
        DataPersistenceManager.instance.NewGame();
    }
    public void OnLoadGameClicked()
    {
        DataPersistenceManager.instance.LoadGame();
    }
    public void OnSaveGameClicked()
    {
        DataPersistenceManager.instance.SaveGame();
    }
}
