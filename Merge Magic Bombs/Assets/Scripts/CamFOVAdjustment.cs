using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFOVAdjustment : MonoBehaviour
{
    [SerializeField] private Renderer boundary;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = boundary.bounds.size.x / boundary.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            mainCam.fieldOfView = boundary.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            mainCam.fieldOfView = boundary.bounds.size.y / 2 * differenceInSize;
        }
    }
}
