using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class Question
{
    public string prompt;
    public string location;
    public string funFact;
    public string question;
    public string answerA;
    public string answerB;
    public string correctAnswer;
}

[System.Serializable]
public class Questions
{
    public List<Question> AllQuestions = new List<Question>();
}


public class JsonDeserialization : MonoBehaviour
{
    private readonly string filename = "data.json";
    
    public Questions JsonObjects()
    {
        string finalPath = Path.Combine(Application.persistentDataPath, filename);

        if (!File.Exists(finalPath))
        {
            Debug.LogError("File Path Does not Exist");
            return default;
        }
        string jsonData = File.ReadAllText(finalPath);
        return JsonUtility.FromJson<Questions>(jsonData);

    }
}
