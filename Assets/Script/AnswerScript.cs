using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QUIZMANAGER quizmanager;
    public void Answer()
    {
        if(isCorrect)
        {
            Debug.Log("Correct Answer");
            quizmanager.correct();
        }
        else
        {
            Debug.Log("WrongAnswer");
            quizmanager.wrong();
        }
    }
}
