using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class ARManagd : MonoBehaviour
{
    public float RotationSpeed = 15f;
    public Button Rightbutton;
    public Button LeftButton;
    public Button InfoButton;
    public Button AudioButton;

    private GameObject WorldObject;
    private GameObject CanvasInfo;
    private GameObject CameraObj;
    private AudioSource Audio;

    private void Start()
    {
        Rightbutton.onClick.AddListener(RotateRight);
        LeftButton.onClick.AddListener(RotateLeft);
        InfoButton.onClick.AddListener(ShowInfo);
        AudioButton.onClick.AddListener(PlayAudio);
        
        CameraObj = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (Time.frameCount % 20 == 0)
        {
            if (WorldObject == null)
            {
                WorldObject = GameObject.FindGameObjectWithTag("ModelObject");
                Audio = WorldObject.transform.GetChild(1).GetComponent<AudioSource>();
                CanvasInfo = GameObject.FindGameObjectWithTag("ModelUI");
                CanvasInfo.gameObject.SetActive(false);
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
