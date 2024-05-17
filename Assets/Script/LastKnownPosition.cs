using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastKnownPosition : MonoBehaviour
{
    public void setLastLocation(string stage, float xPos, float yPos)
    {

        PlayerPrefs.SetFloat(stage + "lastPositionX", xPos);
        PlayerPrefs.SetFloat(stage + "lastPositionY", yPos);
    }

    /*public void getLastLocation(string stage)
    {
        if (PlayerPrefs.HasKey(stage + "lastPositionX"))
        {
            float x = PlayerPrefs.GetFloat(stage + "lastPositionX");
            float y = PlayerPrefs.GetFloat(stage + "lastPositionY");

        }
    }*/
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
}
