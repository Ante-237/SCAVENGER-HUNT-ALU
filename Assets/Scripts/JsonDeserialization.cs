using System;
using System.Collections;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class JsonDeserialization : MonoBehaviour
{
    private readonly string filename = "data.json";
    
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
        QUESTIONS_SO SO_Object = AssetDatabase.LoadAssetAtPath<QUESTIONS_SO>("Assets/Scripts/QUESTIONS_SO.asset");

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