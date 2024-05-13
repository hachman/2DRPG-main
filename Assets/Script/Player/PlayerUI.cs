using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject playerUIGameObject;
    private void Start()
    {
        if (QuizGameManager.instance != null)
        {
            QuizGameManager.instance.OnGameStarted += () => playerUIGameObject.SetActive(false);
            QuizGameManager.instance.OnGameFinished += () => playerUIGameObject.SetActive(true);
        }

    }
}
