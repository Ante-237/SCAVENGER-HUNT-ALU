using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    public QUESTIONS_SO data;
    public Button ReadButton;
    public TextMeshProUGUI TextMesh;


    private void Start()
    {
        ReadButton.onClick.AddListener(changeTest);
    }

    void changeTest()
    {
        TextMesh.text = data.questions[0].question;
    }
}
