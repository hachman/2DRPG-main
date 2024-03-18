using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AdaptivePerformance.VisualScripting;
using UnityEngine.SceneManagement;

public class LevelMove_ref : MonoBehaviour
{
    public int sceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger Entered");

       // if(other.tag == "Player")
        {
            print("Switching Scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            
        }
    }
}
