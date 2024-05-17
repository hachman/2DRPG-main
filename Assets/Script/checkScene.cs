using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class checkScene : MonoBehaviour
{
    string prevScene, currentScene;
    public Vector3 checkLastScene()
    {
        Vector3 pos = new Vector3();
        prevScene = PlayerPrefs.GetString("previousScene");
        currentScene = SceneManager.GetActiveScene().name;

        if (prevScene == "House Outside" && currentScene == "House")
        {
            pos = new Vector3(97, -11, 0);
        }
        else if (prevScene == "Forest" && currentScene == "House Outside")
        {
            pos = new Vector3(32, -30, 0);
        }
        else if (prevScene == "Cave" && currentScene == "Forest")
        {

        }
        else if (prevScene == "Cloud Village" && currentScene == "Cave")
        {

        }

        Debug.Log(prevScene);

        return pos;
    }
}
