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
}
