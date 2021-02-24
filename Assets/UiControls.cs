using System.Linq;
using TMPro;
using UnityEngine;

public class UiControls : MonoBehaviour
{
    private TMP_Text countdown;
    private bool platformIsPC;
    private bool platformIsMobile;

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

    private void Start()
    {
        countdown = GameObject.Find("countdown").GetComponent<TMP_Text>();
    }

    public void SetCountdown(float t)
    {
        countdown.SetText(t.ToString("0.0"));
    }

    public Vector2? GetTouchInput()
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
            return new Vector2(pointClick.Value.x, pointClick.Value.y);
        }
        else
        {
            return null;
        }
    }
    
    
}
