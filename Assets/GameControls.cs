using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameControls : MonoBehaviour
{
    public float stepDuration = 3.0f;
    
    private Hands hands;
    private UiControls _uiControls;
    private bool stepInProgress;
    private float stepCountDown;

    void Start()
    {
        hands = GameObject.FindObjectOfType<Hands>();
        _uiControls = GameObject.FindObjectOfType<UiControls>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectClicks();
        if (stepInProgress)
        {
            stepCountDown -= Time.deltaTime;
            if (stepCountDown <= 0)
            {
                stepCountDown = 0;
                stepInProgress = false;
                hands.ReleaseHands(true);
            }
            _uiControls.SetCountdown(stepCountDown);
        }
    }
    
    void DetectClicks()
    {
        var pointClick = _uiControls.GetTouchInput();

        if (pointClick != null)
        {
            var p = new Vector2(pointClick.Value.x, pointClick.Value.y);
            var i = hands.CheckClickedHand(ref p);
            hands.SelectHand(i);
            stepCountDown = stepDuration;
            stepInProgress = true;
            _uiControls.SetCountdown(stepCountDown);
        }
    }
}
