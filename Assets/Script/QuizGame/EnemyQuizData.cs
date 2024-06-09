using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizData", menuName = "New Quiz Data")]
[Serializable]
public class EnemyQuizData : ScriptableObject
{

    public List<Question> questions;
    public float timer;
    public bool Iseasy = false;
    public bool IsAveraage = false;
    public bool IsBoss = false;
    public int Iseaylives = 1;
    public int IsAverageLives = 3;
    public int IsBossLives = 8;
}
[System.Serializable]
public class Question
{
    public Sprite question;
    public List<Sprite> choices;
    public int correctChoice;
    public Sprite solution;
}




