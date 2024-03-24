using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
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
    public List<Question> questions = new List<Question>();
}


public class JsonDeserialization : MonoBehaviour
{
    private readonly string filename = "data.json";
    public QUESTIONS_SO AllData;
    
    public Questions JsonObjects()
    {
        string finalPath = Path.Combine(Application.dataPath, filename);
        Debug.Log(finalPath);

        if (!File.Exists(finalPath))
        {
            Debug.LogError("File Path Does not Exist");
            return default;
        }
        string jsonData = File.ReadAllText(finalPath);
        return JsonUtility.FromJson<Questions>(jsonData);

    }
    

#if UNITY_EDITOR
    private void Start()
    {
        /*
        QUESTIONS_SO SO_Object = AssetDatabase.LoadAssetAtPath<QUESTIONS_SO>("Assets/Data.asset");

        Questions TemporalQuestion = JsonObjects();
        foreach (Question q in TemporalQuestion.questions)
        {
            SO_Object.questions.Add(q);
        }
        
        AssetDatabase.SaveAssets();
        */

    }
#endif
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QUESTIONS_SO", order = 1)]
public class QUESTIONS_SO : ScriptableObject
{
    public List<Question> questions = new List<Question>();
}
