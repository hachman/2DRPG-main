using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;

public interface IQuizData
{
    void SetupQuizData(List<Question> questions, float timer);
}

public class QuizGameManager : MonoBehaviour, IQuizData
{
    public static QuizGameManager instance { get; set; }
    public Image questionImage;
    public List<Button> choices;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timerText;
    public GameObject correctFeedback;
    public GameObject wrongFeedback;
    public GameObject canvasGameObject;
    public GameObject introPanel;
    // [SerializeField] Image playerImage;
    [SerializeField] Image enemyImage;
    public float timePerQuestion = 10f;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private int lives = 3;
    private float timer;
    private bool quizRunning = false;

    private List<Question> quizQuestions = new();
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StopQuiz(); // Stop the quiz initially
    }

    void Update()
    {
        if (quizRunning)
        {
            timerText.text = "Time: " + Mathf.Round(timer);
        }
    }

    IEnumerator Countdown()
    {
        while (timer > 0 && quizRunning)
        {
            yield return new WaitForSeconds(1);
            timer -= 1;
            timerText.text = "Time: " + Mathf.Round(timer);
        }
        if (quizRunning)
        {
            CheckAnswer(-1); // Timeout, -1 indicates no answer
        }
    }

    void UpdateQuestionUI()
    {
        if (quizQuestions == null) return;
        Question currentQuestion = quizQuestions[currentQuestionIndex];
        questionImage.sprite = currentQuestion.question;

        for (int i = 0; i < choices.Count; i++)
        {
            choices[i].image.sprite = currentQuestion.choices[i];
        }
    }
    Coroutine countdown;
    Action<bool> BattleSuccess;
    public void StartQuiz(Action<bool> OnBattleSuccess)
    {

        BattleSuccess = OnBattleSuccess;
        canvasGameObject.SetActive(true);



        if (!quizRunning)
        {
            StartCoroutine(StartQuizWithIntroPanel());

        }
    }

    public void StartQuiz(Sprite enemySprite, Action<bool> OnBattleSuccess)
    {
        BattleSuccess = OnBattleSuccess;
        canvasGameObject.SetActive(true);
        AudioManager.instance.PlayBackgroundMusic("battle");

        enemyImage.sprite = enemySprite;

        if (!quizRunning)
        {
            StartCoroutine(StartQuizWithIntroPanel());

        }
    }
    IEnumerator StartQuizWithIntroPanel()
    {
        introPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        introPanel.SetActive(false);
        quizRunning = true;
        currentQuestionIndex = 0;
        score = 0;
        lives = 3;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
        timer = timePerQuestion;
        countdown = StartCoroutine(Countdown());
        UpdateQuestionUI();



    }

    public void StopQuiz()
    {
        AudioManager.instance.PlayPreviousBackgroundMusic();
        quizRunning = false;
        StopAllCoroutines();
        answered = false;
        timerText.text = "Time: 0";
        quizQuestions.Clear();
        correctFeedback.SetActive(false);
        wrongFeedback.SetActive(false);
        canvasGameObject.SetActive(false);
        introPanel.SetActive(false);
    }
    bool answered = false;
    public void CheckAnswer(int choiceIndex)
    {
        Debug.Log("Answered");

        if (quizRunning)
        {
            if (answered) return;
            answered = true;
            StopCoroutine(countdown);
            // timer = 0;

            if (choiceIndex == quizQuestions[currentQuestionIndex].correctChoice)
            {
                score++;
                scoreText.text = "Score: " + score;
                correctFeedback.SetActive(true);
            }
            else
            {
                lives--;
                livesText.text = "Lives: " + lives;
                wrongFeedback.SetActive(true);
            }

            StartCoroutine(NextQuestion());
        }
    }

    IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(1.5f);

        correctFeedback.SetActive(false);
        wrongFeedback.SetActive(false);

        if (quizRunning)
        {
            if (currentQuestionIndex < quizQuestions.Count - 1 && lives > 0)
            {
                currentQuestionIndex++;
                UpdateQuestionUI();
                timer = timePerQuestion;
                countdown = StartCoroutine(Countdown());
            }
            else
            {
                EndGame();
            }
            answered = false;
        }

    }
    public void Surrender()
    {
        StopQuiz();
        BattleSuccess?.Invoke(false);
    }

    void EndGame()
    {

        StopQuiz(); // Stop the quiz when it ends

        if (lives > 0)
        {
            BattleSuccess?.Invoke(true);
        }
        else
        {
            BattleSuccess?.Invoke(false);
        }
        // Handle end of game logic here, such as displaying final score, resetting the game, etc.
    }
    private void OnEnable()
    {
        foreach (var item in choices)
        {
            item.onClick.AddListener(() => CheckAnswer(choices.IndexOf(item)));
        }
    }
    private void OnDisable()
    {
        foreach (var item in choices)
        {
            item.onClick.RemoveListener(() => CheckAnswer(choices.IndexOf(item)));
        }
    }
    public void SetupQuizData(List<Question> questions, float timer)
    {
        quizQuestions.AddRange(questions);
        timePerQuestion = timer;
        correctFeedback.SetActive(false);
        wrongFeedback.SetActive(false);

    }


}

[System.Serializable]
public class Question
{
    public Sprite question;
    public List<Sprite> choices;
    public int correctChoice;
}

