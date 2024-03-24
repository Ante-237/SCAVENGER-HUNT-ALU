using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    public JsonDeserialization DataStorage;
    public ARCameraManager ARCM;
    public XROrigin XRO;
    public Camera MainCamera;
    public UIManager UM;
    public QUESTIONS_SO allData;
  
    
    public const int POINTS = 300;
    
    private static int CurrentPoints = 0;
    private float CurrentTime = 0;
    private int CurrentQuestionIndex = 0;

    void NullCheck()
    {
        Assert.IsNotNull(DataStorage);
        Assert.IsNotNull(ARCM);
        Assert.IsNotNull(XRO);
        Assert.IsNotNull(MainCamera);
        Assert.IsNotNull(UM);
    }

    private void Start()
    {
        NullCheck();
        
        
        DisableCameraState();
        
        
        UM.AddListerHuntType(FirstCall);
        UM.AddListerScanButton(UpdateCameraState);
        UM.AddListerScanButton(UpdateDataQuestions);
        UM.AddListerAnswerOne(DisableCameraState);

    }

    private void DisableCameraState()
    {
        ARCM.enabled = false;
        XRO.Camera = null;
        MainCamera.enabled = false;
    }

    private void FirstCall()
    {
        UM.UpdatePrompt(allData.questions[CurrentQuestionIndex].prompt);
    }

    private void UpdateDataQuestions()
    {
        UM.UpdateQuestion(allData.questions[CurrentQuestionIndex].question);
        UM.UpdateAnswers(allData.questions[CurrentQuestionIndex].answerA, allData.questions[CurrentQuestionIndex].answerB);
    }

    private void UpdateCameraState(){
        ARCM.enabled = true;
        XRO.Camera = MainCamera;
        MainCamera.enabled = true;
    }
    
    
    
}
