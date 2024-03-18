using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyNPC : MonoBehaviour, Interactable
{
    [Header("Visual")]
    [SerializeField] private GameObject visualcue;
    [SerializeField] SpriteRenderer characterVisual;

    [Header("Enemy Quiz Data")]
    public EnemyQuizData questionFight;
    [Header("Enemy Death Quest")]
    [SerializeField] Quest_Data questDataToProgress;
    private bool playerInRange;
    private void Awake()
    {
        playerInRange = false;
        visualcue.SetActive(false);
    }
    private void Update()
    {
        if (playerInRange)
        {
            visualcue.SetActive(true);
            /* if (InputManager.GetInstance().GetInteractPressed())
             {
                 Debug.Log(inkJSON.text);
             }*/

        }
        else
        {
            visualcue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }

    }
    bool isInteractable = true;

    public void Interact()
    {
        if (!isInteractable) return;
        isInteractable = false;
        Debug.Log("Fight Enemy " + transform.name);
        QuizGameManager.instance.SetupQuizData(questionFight.questions, questionFight.timer);
        QuizGameManager.instance.StartQuiz(characterVisual.sprite, OnBattleDone);

    }

    public void OnBattleDone(bool isSuccess)
    {
        if (isSuccess)
        {

            StartCoroutine(DieEnemy());
        }
        else
        {
            LoadGame();
            isInteractable = true;
        }

    }
    private void LoadGame()
    {


        QuestSystemManager.instance.RepositionPlayer = true;
        SceneManager.LoadScene(QuestSystemManager.instance.questDatabaseJSON.savedScene);
    }
    IEnumerator DieEnemy()
    {
        if (QuestSystemManager.instance.activeQuest != null)
        {
            if (questDataToProgress != null && questDataToProgress == QuestSystemManager.instance.activeQuest)
            {
                questDataToProgress.Progress();
            }
        }


        yield return new WaitForSeconds(1f);
        // Die

        StopAllCoroutines();
        Destroy(gameObject);
    }
}
