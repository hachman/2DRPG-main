using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance = null;
    [SerializeField] GameObject loadingObject; // Reference to the loading object to activate
    [SerializeField] TMP_Text loadingText; // Reference to the TextMeshProUGUI for displaying loading percentage
    [SerializeField] float delayAfterLoading = .5f; // Delay after loading is done
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Load a scene with optional action upon completion
    public void LoadScene(string sceneName, Action onLoadingDone = null)
    {
        if (loadingObject != null)
            loadingObject.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(UpdateLoadingPercentage(asyncLoad));

        asyncLoad.completed += operation =>
        {
            if (loadingObject != null)
                Invoke("DeactivateLoadingObject", delayAfterLoading);

            onLoadingDone?.Invoke();
        };
    }

    // Coroutine to update loading percentage in TextMeshProUGUI
    private IEnumerator UpdateLoadingPercentage(AsyncOperation asyncOperation)
    {

        while (!asyncOperation.isDone)
        {
            if (loadingText != null)
            {
                loadingText.text = "Loading " + (asyncOperation.progress * 100f).ToString("0.0") + "%";
            }
            yield return null;
        }
    }

    // Deactivate the loading object
    private void DeactivateLoadingObject()
    {
        if (loadingObject != null)
            loadingObject.SetActive(false);
    }
}
