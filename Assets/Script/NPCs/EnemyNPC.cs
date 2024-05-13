using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyNPC : MonoBehaviour, Interactable
{
    [Header("Visual")]
    [SerializeField] private GameObject visualcue;
    [SerializeField] SpriteRenderer characterVisual;
    [SerializeField] float detectRange = 2.5f;
    [SerializeField] LayerMask layerMaskDetect;

    [Header("Enemy Quiz Data")]
    public EnemyQuizData questionFight;
    [Header("Enemy Death Quest")]
    [SerializeField] Quest_Data questDataToProgress;
    private bool playerInRange;
    [SerializeField] UnityEvent OnEnemyWins;
    [SerializeField] UnityEvent OnEnemyDeath;
    [SerializeField] Animation dieAnim;
    [SerializeField] float destroyDelay = .5f;
    private void Awake()
    {
        playerInRange = false;
        visualcue.SetActive(false);
    }
    private void Update()
    {

    }


    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(transform.position, detectRange, layerMaskDetect))
        {
            visualcue?.SetActive(true);
        }
        else
        {
            visualcue?.SetActive(false);
        }
    }
    bool isInteractable = true;

    public void Interact()
    {
        if (!isInteractable) return;
        isInteractable = false;
        Debug.Log("Fight Enemy " + transform.name);
        QuizGameManager.instance.SetupQuizData(questionFight.questions, questionFight.timer);
        QuizGameManager.instance.StartQuiz(characterVisual.sprite, OnBattleDone, questionFight);

    }
    [Button]
    public void DieUnit() => OnBattleDone(true);
    public void OnBattleDone(bool isSuccess)
    {
        if (isSuccess)
        {
            OnEnemyDeath?.Invoke();
            dieAnim.Play();
            StartCoroutine(DieEnemy());
        }
        else
        {
            OnEnemyWins?.Invoke();// where load game is attached
            isInteractable = true;
        }

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


        yield return new WaitForSeconds(destroyDelay);
        // Die

        StopAllCoroutines();
        Destroy(gameObject);
    }
}
