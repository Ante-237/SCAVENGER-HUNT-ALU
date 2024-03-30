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
    [Header("Disorganized Naming")]
    public JsonDeserialization DataStorage;
    public ARCameraManager ARCM;
    public XROrigin XRO;
    public Camera MainCamera;
    public UIManager UM;
    public QUESTIONS_SO allData;
    [Range(0, 10)] public float CheckAnswerTime = 1.0f;
    
    
    public const int POINTS = 300;
    private static  int CurrentPoints = 0;
    private float CurrentTime = 0;
    private int CurrentQuestionIndex = 0;

    private bool FirstScan = false;
    private int NumberOfQuestions = 0;

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
        CountQuestions();
        UM.AddListerHuntType(FirstCall);
        UM.AddListerScanButton(UpdateCameraState);
        UM.AddListerScanButton(UpdateDataQuestions);
        UM.AddListerScanButton(TrackFirstScan);
        UM.AddListerAnswerOne(DisableCameraState);
        UM.AddListerAnswerOne(UpdateAnswerSelection);
        UM.AddListerAnswerTwo(UpdateAnswerSelection);
        UM.AddListerResetButton(ResetStates);
    }

    private void ResetStates()
    {
        
        // set default values to zero
        // update the UI panels to reflect the values
        
        CurrentQuestionIndex = 0;
        CurrentPoints = 0;
        CurrentTime = 0;
        
        UM.UpdatePoints(CurrentPoints);
        UM.UpdatePromptPoint(CurrentPoints);
        UM.UpdateOldText(CurrentPoints);
    }

    private void CountQuestions()
    {
        NumberOfQuestions = allData.questions.Count;
    }

    private void Update()
    {
        if (FirstScan)
        {
            CurrentTime += Time.deltaTime;
            UM.UpdateTimerText(CurrentTime);
        }
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

    private void TrackFirstScan()
    {
        FirstScan = true;
    }

    private void UpdateDataQuestions()
    {
        if (CurrentQuestionIndex < NumberOfQuestions)
        {
            UM.UpdateQuestion(allData.questions[CurrentQuestionIndex].question);
            UM.UpdateAnswers(allData.questions[CurrentQuestionIndex].answerA, allData.questions[CurrentQuestionIndex].answerB);
            CurrentQuestionIndex++;
        }
      
    }
    
    private void UpdateCameraState(){
        ARCM.enabled = true;
        XRO.Camera = MainCamera;
        MainCamera.enabled = true;
    }

    public void UpdateAnswerSelection()
    {
        // call the methods to get the current answer and compare with the right answer.
        // call coroutine to update the panels if there are still questions running. 
        // update the score depending on the answer choice.
        // pass in the lag time for calling the coroutine.
        string presentAnswer =  UM.GetCurrentAnswer();
        // Debug.Log("Current Answer :"  + presentAnswer  + "Correct Answer :" + allData.questions[CurrentQuestionIndex - 1].correctAnswer);
        
        if (presentAnswer == allData.questions[CurrentQuestionIndex - 1].correctAnswer)
        {
            CurrentPoints += 100;
            // Debug.Log("Current Points : " + CurrentPoints);
        }

       if (CurrentQuestionIndex < NumberOfQuestions)
       {
         
           StartCoroutine(checkAnswerMove(CheckAnswerTime));
       }
       else
       {
           // call coroutine to show final panel with final score and time. 
           StartCoroutine(FinalScoreUpdate(CheckAnswerTime));
       }
    }

    IEnumerator checkAnswerMove(float lagTime)
    {
        // update score
        // restart all panels again from prompt
        // update the current prompts questions
        // increment the index  checking the various questions
        yield return new WaitForSeconds(lagTime);
        UM.UpdatePrompt(allData.questions[CurrentQuestionIndex].prompt);
        UM.UpdatePoints(CurrentPoints);
        UM.UpdatePromptPoint(CurrentPoints);
        UM.UpdateOldText(CurrentPoints);
        UM.RestartScanningPanel();
        
    }
    
    IEnumerator FinalScoreUpdate(float lagTime)
    {
        // update time and score
        yield return new WaitForSeconds(lagTime);
        UM.CongratPageSelected();
        UM.UpdateFinalScoreText(CurrentPoints);
        UM.UpdateFinalTimeText(CurrentTime);
    }
    
}
