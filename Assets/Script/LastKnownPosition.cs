using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastKnownPosition : MonoBehaviour
{
    /*
    public void setLastLocation(string stage, float xPos, float yPos)
    {

        PlayerPrefs.SetFloat(stage + "lastPositionX", xPos);
        PlayerPrefs.SetFloat(stage + "lastPositionY", yPos);
    }

    public void getLastLocation(string stage)
    {
        if (PlayerPrefs.HasKey(stage + "lastPositionX"))
        {
            float x = PlayerPrefs.GetFloat(stage + "lastPositionX");
            float y = PlayerPrefs.GetFloat(stage + "lastPositionY");

        }
    }
    public Vector3 getLastLocation(string stage)
    {
        Vector3 pos = new Vector3();

        if (PlayerPrefs.HasKey(stage + "lastPositionX"))
        {
            pos.x = PlayerPrefs.GetFloat(stage + "lastPositionX");
            pos.y = PlayerPrefs.GetFloat(stage + "lastPositionY");

            
        }

        return pos;
    }
    */
    Vector3[] positions = new[]
    {
        new Vector3(97 - 32.95f,-11 - 1.73f ,0),
        new Vector3(1,2,3)
    };
    public void loadLastLocation(string prevScene, Transform Player)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (prevScene != null)
        {
            if(prevScene == "House Outside" && currentScene == "House")
            {
                Player.position = positions[0];
            }
        }
    }
}
