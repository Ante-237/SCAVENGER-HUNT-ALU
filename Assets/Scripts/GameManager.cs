using System;
using System.Collections;
using System.Collections.Generic;
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

    void NullCheck()
    {
        Assert.IsNotNull(DataStorage);
        Assert.IsNotNull(ARCM);
        Assert.IsNotNull(XRO);
        Assert.IsNotNull(MainCamera);
    }

    private void Start()
    {
        NullCheck();
        ARCM.enabled = false;
        XRO.Camera = null;
        MainCamera.enabled = false;
    }
}
