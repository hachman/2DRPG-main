
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    public GameData gameData;

   public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            Debug.LogError("Found More than one Data Persistence Manager in the Scene.");
        }
        instance = this;
    }
    private void Start()
    {
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        // todo - loa any saved data from a file using data handler 
        // if no data can be load, initialize to a new game 
        if(this.gameData == null)
        {
            Debug.Log("No data was found. Initialize to Defaults.");
            NewGame();
        }
        // todo push the loaded data to all other scripts that need it
    }
    public void SaveGame()
    {
        // ToDo - pass the data to other scripts so they can update it 

        // ToDo - Save that data to a file using the data handler 

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
   
