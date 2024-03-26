using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.ARFoundation;

public class ARManager : MonoBehaviour
{
    [Header("AR Manager Settings")] [SerializeField]
    private string[] TrackedNames = { "angola", "social_commons" };

    [SerializeField] private ARTrackedImageManager TrackedImageManager;
    [SerializeField] private UIManager UIM;
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
            Debug.Log("Tracked Image is :" + tracked.name);
            
            if (tracked.name == TrackedNames[0])
            {
              Debug.Log("First Tracking Image Detected");
            }

            if (tracked.name == TrackedNames[1])
            {
               Debug.Log("Second Tracking image Detected");
            }
        }
    }
    
    
}
