using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string sceneToGo;
    [SerializeField] Button button;


    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(GoToScene);
        }
    }
    private void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(GoToScene);
        }
    }
    private void GoToScene()
    {
        SceneManager.LoadScene(sceneToGo);
    }
}
