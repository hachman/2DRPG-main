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
    [SerializeField] Image bossDiplayImage;
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
    int bossLives = 0;
    private List<Question> quizQuestions = new();
    public DifficultyLevel difficultyLevel;
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
    public void StartQuiz(DifficultyLevel difficulty, Action<bool> OnBattleSuccess)
    {
        difficultyLevel = difficulty;
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

            if (difficultyLevel == DifficultyLevel.Easy && score >= 1) //to kill you have to answer 1 correct answer out of 2 
            {
                
                EndGame();
            }
            else if (difficultyLevel == DifficultyLevel.Average && score >= 3) // to kill you have to answer 3 correct answer out of 5
            {
                EndGame();
            }
            else if (difficultyLevel == DifficultyLevel.Hard && currentQuestionIndex >= bossLives) // it defend to the boss live to eliminate out of 10
            {
                EndGame();
            }

        }
    }
    public void ContinueGame() => SetupNextItem(); // no delay, next immediately

    IEnumerator BossKilled()
    {
        AudioManager.instance.PlayBackgroundMusic("victory");
        bossKilledPanel?.SetActive(true);
        bossDiplayImage.sprite = enemyImage.sprite;
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
    public enum DifficultyLevel
    {
        Easy,
        Average,
        Hard
    }


}
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.Events;

public interface IQuizData
{
    void SetupQuizData(List<Question> questions, float timer);
}

public class QuizGameManager : MonoBehaviour, IQuizData
{
    public static QuizGameManager instance { get; private set; }

    [Header("-- BOSS REF --")]
    [SerializeField] private GameObject bossKilledPanel;
    [SerializeField] private Image bossDiplayImage;
    [SerializeField] private TextMeshProUGUI bossLivesTMP;

    [Header("QUIZ REF")]
    public Image questionImage;
    public List<Button> choices;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timerText;
    public GameObject correctFeedback;
    public GameObject wrongFeedback;
    [SerializeField] private Image solutionImage;
    public GameObject canvasGameObject;
    public GameObject introPanel;
    [SerializeField] private Image enemyImage;
    [SerializeField] private UnityEvent _OnBossKilled;

    [Header(" --- Correct UI PARAMETERS ---")]
    [SerializeField] private TextMeshProUGUI phraseDisplay;
    [SerializeField] private string[] randomPhrases;
    public float timePerQuestion = 10f;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private int lives = 3;
    private float timer;
    private bool quizRunning = false;
    private bool answered = false;

    public Action OnBossKilled;
    public Action OnGameStarted;
    public Action OnGameFinished;
    private EnemyQuizData currentData;
    private int bossLives = 0;
    private List<Question> quizQuestions = new List<Question>();
    public DifficultyLevel difficultyLevel;

    private Coroutine countdown;
    private Action<bool> BattleSuccess;


    [SerializeField] GameObject SolutionPanel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StopQuiz(); // Stop the quiz initially
    }

    private void Update()
    {
        if (quizRunning)
        {
            timerText.text = "Time: " + Mathf.Round(timer);
        }
    }

    private IEnumerator Countdown()
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

    private void UpdateQuestionUI()
    {
        if (quizQuestions == null) return;

        bossLivesTMP.gameObject.SetActive(currentData.IsBoss);
        if (currentData.IsBoss)
        {
            bossLivesTMP.text = $"BOSS LIVES: {bossLives}";
        }

        Question currentQuestion = quizQuestions[currentQuestionIndex];
        questionImage.sprite = currentQuestion.question;

        for (int i = 0; i < choices.Count; i++)
        {
            choices[i].image.sprite = currentQuestion.choices[i];
        }
    }

    public void StartQuiz(DifficultyLevel difficulty, Action<bool> OnBattleSuccess)
    {
        difficultyLevel = difficulty;
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

    private IEnumerator StartQuizWithIntroPanel()
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

        Debug.Log("Nawala");
    }
    public void CheckAnswer(int choiceIndex)
    {
        Debug.Log("Answered");

    if (quizRunning && !answered)
    {
        answered = true;
        StopCoroutine(countdown);

        if (choiceIndex == quizQuestions[currentQuestionIndex].correctChoice) // Correct answer
        {
            HandleCorrectAnswer();
            correctFeedback.SetActive(true);
        }
        else // Wrong answer
        {
            HandleWrongAnswer();
        }
            
        }
}

private void HandleCorrectAnswer()
{
    score++;
    scoreText.text = "Score: " + score;

    DisplayPhraseAndSolution();

    Debug.Log("HandleCorrectAnswer accessed");

    try
    {
        Debug.Log("Setting correctFeedback active");
        correctFeedback.SetActive(true);
        Debug.Log("correctFeedback is now active");
    }
    catch (Exception e)
    {
        Debug.LogError("Error displaying correct feedback: " + e.Message);
    }

    if (currentData.IsBoss)
    {
        bossLives--;
        if (bossLives <= 0)
        {
            Debug.Log("Patay");
            StartCoroutine(BossKilled());
        }
    }
    StopCoroutine(countdown);
    // Optionally, show a button to proceed to the next question
}

    private void HandleWrongAnswer()
    {
    lives--;
    livesText.text = "Lives: " + lives;
    DisplayPhraseAndSolution();
    wrongFeedback.SetActive(true);
        StopCoroutine(countdown);
        // Optionally, show a button to proceed to the next question
    }

    private void DisplayPhraseAndSolution()
    {
    phraseDisplay.text = randomPhrases[UnityEngine.Random.Range(0, randomPhrases.Length)];
        if (quizQuestions[currentQuestionIndex].solution != null)
        {
        solutionImage.sprite = quizQuestions[currentQuestionIndex].solution;
        }
        if (countdown != null)
        {
            StopCoroutine(countdown);
        }

        CheckEndGameConditions();
    }


    private void CheckEndGameConditions()
    {
        if (difficultyLevel == DifficultyLevel.Easy && score >= 1)
        {
            //StopCoroutine(countdown);  
            EndGame();
        }
        else if (difficultyLevel == DifficultyLevel.Average && score >= 3)
        {
            EndGame();
        }
        else if (difficultyLevel == DifficultyLevel.Hard && currentQuestionIndex >= bossLives)
        {
            EndGame();
        }
    }

    private void ShowAnswerAnalysis()
    {
        // Implement analysis display logic here if needed
    }


    public void ContinueGame() => SetupNextItem();

    private IEnumerator BossKilled()
    {
        AudioManager.instance.PlayBackgroundMusic("victory");
        bossKilledPanel?.SetActive(true);
        bossDiplayImage.sprite = enemyImage.sprite;
        yield return new WaitForSeconds(3f);
        OnBossKilled?.Invoke();
        _OnBossKilled?.Invoke();
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

    private void EndGame()
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

    public enum DifficultyLevel
    {
        Easy,
        Average,
        Hard
    }
}