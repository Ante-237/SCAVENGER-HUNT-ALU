using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.ARFoundation;

public class ARManager : MonoBehaviour
{
    [Header("AR Manager Settings")] [SerializeField]
    private string[] TrackedNames = { "angola", "social_commons" };

    [SerializeField] private ARTrackedImageManager TrackedImageManager;
    [SerializeField] private UIManager UIM;
    [SerializeField] private TextMeshProUGUI DebugText;
    public bool startedTracking = false;


    public void Update()
    {
        if (startedTracking)
        {
            UpdateCurrentPrefab();
        }
    }

    private void Start()
    {
        NullCheck();
        DebugText.text = "Started";

        UIM.AddListerScanButton(UpdateTrackingCheck);
        UIM.AddListerAnswerOne(StopTracking);
    }

    void UpdateTrackingCheck()
    {
        startedTracking = true;
    }

    void StopTracking()
    {
        startedTracking = false;
    }

void NullCheck()
    {
        Assert.IsNotNull(TrackedImageManager);
        Assert.IsNotNull(UIM);
    }


    private void UpdateCurrentPrefab()
    {
        
        foreach (ARTrackedImage tracked in TrackedImageManager.trackables)
        {
            
            DebugText.text += "Tracked Image is :" + tracked.name + "\n";
            
            if (tracked.name == TrackedNames[0])
            {
              DebugText.text += "First Tracking Image Detected \n";
            }

            if (tracked.name == TrackedNames[1])
            {
               DebugText.text += "Second Tracking image Detected \n";
            }
        }
    }
    
    
}
