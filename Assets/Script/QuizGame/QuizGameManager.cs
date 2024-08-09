/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.Events;
using Unity.VisualScripting;

public interface IQuizData
{
    void SetupQuizData(List<Question> questions, float timer);
}

public class QuizGameManager : MonoBehaviour, IQuizData
{
    public static QuizGameManager instance { get; set; }


    [Header("-- BOSS REF --")]
    [SerializeField] GameObject bossKilledPanel;
    [SerializeField] TextMeshProUGUI bossLivesTMP;
    [Header("QUIZ REF")]

    public Image questionImage;
    public List<Button> choices;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timerText;
    public GameObject correctFeedback;
    public GameObject wrongFeedback;
    [SerializeField] Image solutionImage;
    public GameObject canvasGameObject;
    public GameObject introPanel;
    // [SerializeField] Image playerImage;
    [SerializeField] Image enemyImage;
    [SerializeField] UnityEvent _OnBossKilled;
    [Header(" --- Correct UI PARAMETERS ---")]
    [SerializeField] TextMeshProUGUI phraseDisplay;
    [SerializeField] string[] randomPhrases;
    public float timePerQuestion = 10f;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private int lives = 3;
    private float timer;
    private bool quizRunning = false;

    public Action OnBossKilled;
    public Action OnGameStarted;
    public Action OnGameFinished;
    EnemyQuizData currentData;
    int easylive = 0;
    int averagelive= 0;
    int bossLives = 0;
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


        if (currentData.IsBoss)
        {
            bossLivesTMP.gameObject.SetActive(true);
            bossLivesTMP.text = string.Format($"BOSS LIVES: {bossLives}");
        }
        else
        {
            bossLivesTMP.gameObject.SetActive(false);

        }
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

    public void StartQuiz(Sprite enemySprite, Action<bool> OnBattleSuccess, EnemyQuizData data)
    {
        currentData = data;
        easylive = data.IsEasylives;
        averagelive = data.IsAverageLives;
        bossLives = data.IsBossLives;
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
        OnGameStarted?.Invoke();

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
        OnGameFinished?.Invoke();
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
        bossKilledPanel.SetActive(false);

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

            if (choiceIndex == quizQuestions[currentQuestionIndex].correctChoice) // correct
            {

                score++;
                scoreText.text = "Score: " + score;
                phraseDisplay.text = randomPhrases[UnityEngine.Random.Range(0, randomPhrases.Length)];
                
                correctFeedback.SetActive(true);

                if (currentData.IsBoss)
                {
                    bossLives--;
                    if (bossLives <= 0)
                    {
                        // display congrats
                        StartCoroutine(BossKilled());

                    }


                }
             

                StartCoroutine(NextQuestion()); // delayed next question
            }
            else // wrong
            {
               
                 lives--;
                 livesText.text = "Lives: " + lives;
                 if (quizQuestions[currentQuestionIndex].solution != null) solutionImage.sprite = quizQuestions[currentQuestionIndex].solution;
                 wrongFeedback.SetActive(true);
            }

            

        }
    }
    public void ContinueGame() => SetupNextItem(); // no delay, next immediately

    IEnumerator BossKilled()
    {
        AudioManager.instance.PlayBackgroundMusic("victory");
        bossKilledPanel?.SetActive(true);
        yield return new WaitForSeconds(3f);
        OnBossKilled?.Invoke(); // action tobe accessed through script
        _OnBossKilled?.Invoke();// unity event to be accessed via inspector
        bossKilledPanel?.SetActive(false);
        EndGame();
        AudioManager.instance.PlayPreviousBackgroundMusic();

    }

    IEnumerator NextQuestion()
    {

        yield return new WaitForSeconds(1f);
        SetupNextItem();


    }
    private void SetupNextItem()
    {
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
 
}*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.Events;
using Unity.VisualScripting;

public interface IQuizData
{
    void SetupQuizData(List<Question> questions, float timer);
}

public class QuizGameManager : MonoBehaviour, IQuizData
{
    public static QuizGameManager instance { get; set; }

    [Header("-- BOSS REF --")]
    [SerializeField] GameObject bossKilledPanel;
    [SerializeField] TextMeshProUGUI bossLivesTMP;
    [Header("-- EASY AND AVERAGE REF --")]
    [SerializeField] TextMeshProUGUI easyLivesTMP;
    [SerializeField] TextMeshProUGUI averageLivesTMP;
    [Header("HEARTS")]
    [SerializeField] List<Image> playerHearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    [Header("EASY ENEMY HEARTS")]
    [SerializeField] List<Image> enemyHearts;
    [SerializeField] List<Image> aenemyHearts;
    [SerializeField] List<Image> benemyHearts;
    [SerializeField] Sprite enemfullHeart;
    [SerializeField] Sprite enememptyHeart;

    [Header("QUIZ REF")]
    public Image questionImage;
    public List<Button> choices;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timerText;
    public GameObject correctFeedback;
    public GameObject wrongFeedback;
    [SerializeField] Image solutionImage;
    public GameObject canvasGameObject;
    public GameObject introPanel;
    [SerializeField] Image enemyImage;
    [SerializeField] UnityEvent _OnBossKilled;
    [Header(" --- Correct UI PARAMETERS ---")]
    [SerializeField] TextMeshProUGUI phraseDisplay;
    [SerializeField] string[] randomPhrases;
    public float timePerQuestion = 10f;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private int lives = 0;
    private int enemyLives = 3;
    private float timer;
    private bool quizRunning = false;

    public Action OnBossKilled;
    public Action OnGameStarted;
    public Action OnGameFinished;
    EnemyQuizData currentData;
    public int easyLives = 0;
    public int averageLives = 0;
    public int bossLives = 0;
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

        if (currentData.IsBoss)
        {
            //lives 8
            bossLivesTMP.gameObject.SetActive(true);
            bossLivesTMP.text = string.Format($"BOSS LIVES:");
        }
        else
        {
            bossLivesTMP.gameObject.SetActive(false);
        }

        if (currentData.IsAverage)
        {
            //lives 3
            averageLivesTMP.gameObject.SetActive(true);
            Debug.Log("Average ka wag kang bobo");
            averageLivesTMP.text = $"ENEMY LIVES:";
        }
        else
        {
            averageLivesTMP.gameObject.SetActive(false);
        }

        if (currentData.IsEasy)
        {
            //lives 1
            easyLivesTMP.gameObject.SetActive(true);
            Debug.Log("Easy ka tanga");
            easyLivesTMP.text = $"ENEMY LIVES:";
        }
        else
        {
            easyLivesTMP.gameObject.SetActive(false);
        }
        livesText.text = $"Lives: "; // Update lives text

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
        lives = 3; // Reset player lives
        UpdateLivesUI();// player live updated
       
        canvasGameObject.SetActive(true);

        if (!quizRunning)
        {
            StartCoroutine(StartQuizWithIntroPanel());
        }
    }

    public void StartQuiz(Sprite enemySprite, Action<bool> OnBattleSuccess, EnemyQuizData data)
    {
        currentData = data;
        easyLives = data.IsEasylives;
        averageLives = data.IsAverageLives;
        bossLives = data.IsBossLives;
        lives = 3;
        UpdateLivesUI();
        UpdateEnemyLivesUI(); // enemy lives 
        BattleSuccess = OnBattleSuccess;
        canvasGameObject.SetActive(true);
        AudioManager.instance.PlayBackgroundMusic("battle");

        enemyImage.sprite = enemySprite;

        if (data.IsEasy)
        {
            enemyLives = easyLives;
        }
        else if (data.IsAverage)
        {
            enemyLives = averageLives;
        }
        else if (data.IsBoss)
        {
            enemyLives = bossLives;
        }

        UpdateEnemyLivesUI(); // enemy lives 
        
        if (!quizRunning)
        {
            StartCoroutine(StartQuizWithIntroPanel());
        }
    }

    IEnumerator StartQuizWithIntroPanel()
    {
        OnGameStarted?.Invoke();

        introPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        introPanel.SetActive(false);
        quizRunning = true;
        currentQuestionIndex = 0;
        score = 0;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
        timer = timePerQuestion;
        countdown = StartCoroutine(Countdown());
        UpdateQuestionUI();
    }

    public void StopQuiz()
    {
        OnGameFinished?.Invoke();
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
        bossKilledPanel.SetActive(false);
        easyLivesTMP.gameObject.SetActive(false);
        averageLivesTMP.gameObject.SetActive(false);
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

            if (choiceIndex == quizQuestions[currentQuestionIndex].correctChoice) // correct
            {
                score++;
                scoreText.text = "Score: " + score;
                phraseDisplay.text = randomPhrases[UnityEngine.Random.Range(0, randomPhrases.Length)];
                if (quizQuestions[currentQuestionIndex].solution != null) solutionImage.sprite = quizQuestions[currentQuestionIndex].solution;
                correctFeedback.SetActive(true);

                if (currentData.IsBoss)
                {

                    bossLives--;
                    UpdateEnemyLivesUI(); // enemy lives 
                    if (bossLives <= 0)
                    {
                        // display congrats
                        StartCoroutine(BossKilled());
                    }
                }
                else if (currentData.IsEasy)
                {
                    easyLives--;
                    UpdateEnemyLivesUI(); // enemy lives 
                    easyLivesTMP.text = $"ENEMY LIVES:";
                    if (easyLives <= 0)
                    {
                        StopCoroutine(GameWon());
                    }
                }
                else if (currentData.IsAverage)
                {
                    averageLives--;
                    UpdateEnemyLivesUI(); // enemy lives 
                    averageLivesTMP.text = $"ENEMY LIVES:";
                    if (averageLives <= 0)
                    {
                        StartCoroutine(GameWon());
                    }
                }
                //StartCoroutine(NextQuestion()); // delayed next question
            }
            else // wrong
            {
                lives--;
                UpdateLivesUI();
                if (quizQuestions[currentQuestionIndex].solution != null) solutionImage.sprite = quizQuestions[currentQuestionIndex].solution;
                wrongFeedback.SetActive(true);
                if (lives <= 0)
                {
                    EndGame();
                }
            }


        }
    }

    private void UpdateEnemyLivesUI()
    {
        int remainingLives = 0;

        if (currentData.IsEasy)
        {
            remainingLives = easyLives;
        }
        else if (currentData.IsAverage)
        {
            remainingLives = averageLives;
        }
        else if (currentData.IsBoss)
        {
            remainingLives = bossLives;
        }

        for (int i = 0; i < enemyHearts.Count; i++)
        {
            if (i < remainingLives)
            {
                enemyHearts[i].sprite = fullHeart;
            }
            else
            {
                enemyHearts[i].sprite = emptyHeart;
            }
        }
    }
    public void ContinueGame() => SetupNextItem(); // no delay, next immediately

    IEnumerator BossKilled()
    {
        AudioManager.instance.PlayBackgroundMusic("victory");
        bossKilledPanel?.SetActive(true);
        yield return new WaitForSeconds(3f);
        OnBossKilled?.Invoke(); // action to be accessed through script
        _OnBossKilled?.Invoke();// unity event to be accessed via inspector
        bossKilledPanel?.SetActive(false);
        EndGame();
        AudioManager.instance.PlayPreviousBackgroundMusic();
    }

    IEnumerator GameWon()
    {
        correctFeedback?.SetActive(true);
        yield return new WaitForSeconds(1f);
        EndGame();

    }

    IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(1f);
        SetupNextItem();
    }
    private void SetupNextItem()
    {
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
    private void UpdateLivesUI()
    {
        for (int i = 0; i < playerHearts.Count; i++)
        {
            if (i < lives)
            {
                playerHearts[i].sprite = fullHeart;
            }
            else
            {
                playerHearts[i].sprite = emptyHeart;
            }
        }
    }
   
}
