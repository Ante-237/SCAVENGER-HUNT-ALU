using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.MagicLeap;

public class ARManagd : MonoBehaviour
{
    [Header("AR Manager Settings")] [SerializeField]
    private string[] TrackedNames = { "angola", "social_commons" };
    [SerializeField] private ARTrackedImageManager TrackedImageManager;


    [Header("Tracked Image Prefabs")] [SerializeField]
    private List<GameObject> DetectionPrefabs = new List<GameObject>();

    private GameObject CurrentTrackedPrefab;
    
    [Header("Reference World Images")]
    public float RotationSpeed = 15f;
    public Button Rightbutton;
    public Button LeftButton;
    public Button InfoButton;
    public Button AudioButton;

    [Header("World Objects")]
    private GameObject WorldObject;
    private GameObject CanvasInfo;
    private GameObject CameraObj;
    private AudioSource Audio;

    private void Start()
    {
        
        // set current track prefab here
        CurrentTrackedPrefab = DetectionPrefabs[1];

        TrackedImageManager = GetComponent<ARTrackedImageManager>();
        
        Rightbutton.onClick.AddListener(RotateRight);
        LeftButton.onClick.AddListener(RotateLeft);
        InfoButton.onClick.AddListener(ShowInfo);
        AudioButton.onClick.AddListener(PlayAudio);
        
        CameraObj = GameObject.FindGameObjectWithTag("MainCamera");


        TrackedImageManager.trackedImagesChanged += (ARTrackedImagesChangedEventArgs args) =>
        {
            TrackedImageManager.trackedImagePrefab = CurrentTrackedPrefab;
        };


    }

    private void UpdateCurrentPrefab()
    {
        
        foreach (ARTrackedImage tracked in TrackedImageManager.trackables)
        {
            Debug.Log("Tracked Image is :" + tracked.name);
            
            if (tracked.name == TrackedNames[0])
            {
               // CurrentTrackedPrefab = DetectionPrefabs[0];
                TrackedImageManager.trackedImagePrefab = CurrentTrackedPrefab;
            }

            if (tracked.name == TrackedNames[1])
            {
                CurrentTrackedPrefab = DetectionPrefabs[1];
                TrackedImageManager.trackedImagePrefab = DetectionPrefabs[1];
            }
        }
    }

    void Update()
    {
        UpdateCurrentPrefab();
        
        if (Time.frameCount % 20 == 0)
        {
            if (WorldObject == null)
            {
                WorldObject = GameObject.FindGameObjectWithTag("ModelObject");
                if (WorldObject.name == "FuseboxMesh")
                {
                    Audio = WorldObject.transform.GetChild(1).GetComponent<AudioSource>();
                    CanvasInfo = GameObject.FindGameObjectWithTag("ModelUI");
                    CanvasInfo.gameObject.SetActive(false);
                }
             
            }
        }

        if (CanvasInfo != null)
        {
            CanvasInfo.transform.LookAt(CameraObj.transform, Vector3.down);
        }
    }

    public void RotateRight()
    {
        if (WorldObject != null)
        {
            WorldObject.transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
            
        }
    }

    public void RotateLeft()
    {
        if (WorldObject != null)
        {
            WorldObject.transform.Rotate(0, -RotationSpeed * Time.deltaTime, 0);
        }
        
    }

    public void ShowInfo()
    {
        if (CanvasInfo != null)
        {
            if (CanvasInfo.gameObject.activeInHierarchy)
            {
                CanvasInfo.gameObject.SetActive(false);
            }
            else
            {
                CanvasInfo.gameObject.SetActive(true);
            }
        }
    }

    public void PlayAudio()
    {
        if (Audio != null)
        {
            Audio.Play();
        }
    }
}
