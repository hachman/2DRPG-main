using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class QUIZMANAGER : MonoBehaviour
{
    public List<QUESTIONANDANSWER> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject Quizpanel;
    public GameObject GoPanel;

    public TextMeshProUGUI QuestionTxt;


    int totalQuestion = 0;
    public int score;

    private void Start()
    {
        totalQuestion = QnA.Count;
        
        generateQuestion();
    }

    void GameOver()
    {
        Quizpanel.SetActive(false);
        GoPanel.SetActive(true);
    }

    public void correct()
    {
        score += 1;
       QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void wrong()
    {

        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswer()
    {
         for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            string v = QnA[currentQuestion].Answer[i];
            options[i].transform.GetChild(0).GetComponent<Text>().text = v;
            
            if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }
    void generateQuestion()
    {
         if (QnA.Count > 0)
         {
             currentQuestion = Random.Range(0, QnA.Count);

             QuestionTxt.text = QnA[currentQuestion].Question;

             SetAnswer();

        }
         else
         {
             Debug.Log("out of question");
         }
    }
}
