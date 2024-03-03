using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToPrevScene : MonoBehaviour
{
     private int prevSceneToLoad;

    private void Start()
    {
        prevSceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(prevSceneToLoad);
    }
}
