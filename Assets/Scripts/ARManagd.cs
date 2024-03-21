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

    private GameObject WorldObject;

    private void Start()
    {
        Rightbutton.onClick.AddListener(RotateRight);
        LeftButton.onClick.AddListener(RotateLeft);
    }

    void Update()
    {
        if (Time.frameCount % 20 == 0)
        {
            if (WorldObject == null)
            {
                WorldObject = GameObject.FindGameObjectWithTag("ModelObject");
            }
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

    
    
    
}
