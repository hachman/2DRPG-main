using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{

    public void LoadPRogress()
    {
        QuestSystemManager.instance?.LoadData(OnDataRetrieved);
    }

    private void OnDataRetrieved(QuestDatabaseJSON jSON)
    {
        if (jSON.savedScene == null || jSON.savedScene == "MainMenuNew") return;

        QuestSystemManager.instance?.SetQuestSystem(jSON);

        FlyHigh.playerLoadPos = new Vector2(jSON.xSavedPosition, jSON.ySavedPosition);

        FlyHigh.IsLoadGame = true;
        //load scene and reposition player
        ChangeScene.instance?.LoadScene(jSON.savedScene, () =>
        {

        });
    }

}
public static class FlyHigh
{
    public static Vector2 playerLoadPos;
    public static bool IsLoadGame = false;

}