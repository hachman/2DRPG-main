using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "QuizData", menuName = "New Quiz Data")]
[Serializable]
public class EnemyQuizData : ScriptableObject
{

    public List<Question> questions;
    public float timer;
    public bool IsEasy = false;
    public bool IsAverage = false;
    public bool IsBoss = false;
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

