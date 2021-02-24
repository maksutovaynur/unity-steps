using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameControls : MonoBehaviour
{
    public float stepDuration = 3.0f;
    
    private Hands hands;
    private bool platformIsPC;
    private bool platformIsMobile;
    private bool stepInProgress;
    private float stepCountDown;
    private void Awake()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.LinuxPlayer:
            case RuntimePlatform.LinuxEditor:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.WebGLPlayer:
                platformIsPC = true;
                break;
            case RuntimePlatform.Android:
                platformIsMobile = true;
                break;
        }
    }

    void Start()
    {
        hands = GameObject.FindObjectOfType<Hands>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stepInProgress)
        {
            stepCountDown -= Time.deltaTime;
            if (stepCountDown <= 0)
            {
                stepCountDown = 0;
                stepInProgress = false;
                hands.ReleaseHands(true);
            }
        }

    }
    
    void DetectClicks()
    {
        Vector3? pointClick = null;
        if (platformIsMobile && Input.touchCount > 0)
        {
            var ts = Input.touches.Where(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);
            if (ts.Any())
            {
                var touch = ts.First();
                pointClick = Camera.main.ScreenToWorldPoint(touch.position);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            pointClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (pointClick != null)
        {
            var p = new Vector2(pointClick.Value.x, pointClick.Value.y);
            var i = hands.CheckClickedHand(ref p);
            hands.SelectHand(i);
            stepCountDown = stepDuration;
            stepInProgress = true;
        }
    }
}
