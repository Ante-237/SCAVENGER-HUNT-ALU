using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QUESTIONS_SO", order = 1)]
public class QUESTIONS_SO : ScriptableObject
{
    public List<Question> questions = new List<Question>();
}