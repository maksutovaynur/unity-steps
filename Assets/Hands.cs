using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Hands : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject handPrefab;
    public float handPrefabScale = 3.0f;
    [FormerlySerializedAs("handStateCount")] public int mineHandsCount = 3;
    public float mineHandsWidth = 0.6f;
    public float mineHandsY = 0.1f;
    private List<HandState> mineHands;
    private List<GameObject> mineHandsObjects;
    private List<Collider2D> mineHandsColliders;
    private bool platformIsPC;
    private bool platformIsMobile;

    private void Awake()
    {        
        mineHandsObjects = new List<GameObject>();
        
        var leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        transform.localScale = new Vector3(Math.Abs(leftBottom.x), Math.Abs(leftBottom.y), transform.localScale.z);
        var y = transform.position.y - transform.localScale.y + mineHandsY;
        var dx = 2 * transform.localScale.x * mineHandsWidth / (mineHandsCount - 1);
        var x = transform.position.x - dx * (mineHandsCount - 1) / 2;
        for (int i = 0; i < mineHandsCount; i++)
        {
            var p = new Vector3(x + dx * i, y,-1.0f);
            var o = Instantiate(handPrefab, p, Quaternion.identity);
            o.transform.localScale = new Vector3(handPrefabScale, handPrefabScale, 1.0f);
            mineHandsObjects.Add(o);
        }
        

    }

    void Start()
    {
        mineHands = new List<HandState>();
        mineHandsColliders = new List<Collider2D>();
        for (int i = 0; i < mineHandsCount; i++)
        {
            var hs = mineHandsObjects[i].GetComponent<HandState>();
            mineHands.Add(hs);
            hs.SetState(i);

            var c = mineHandsObjects[i].GetComponent<Collider2D>();
            mineHandsColliders.Add(c);
        }
    }

    public void ReleaseHands(bool value = false)
    {
        for (int i = 0; i < mineHandsCount; i++)
        {
            mineHandsObjects[i].SetActive(value);
        }
    }
    public void SelectHand(int i)
    {
        if (i < 0 || i >= mineHandsCount) return;
        ReleaseHands(false);
        mineHandsObjects[i].SetActive(true);
    }

    public int CheckClickedHand(ref Vector2 point)
    {
        for (int i = 0; i < mineHandsCount; i++)
        {
            if (mineHandsColliders[i].OverlapPoint(point))
            {
                return i;
            }
        }
        return -1;
    }
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
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
            SelectHand(CheckClickedHand(ref p));
        }
    }
}
