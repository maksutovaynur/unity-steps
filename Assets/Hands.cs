using System;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject handPrefab;
    public int handStateCount = 3;
    public float mineHandsWidth = 0.7f;
    public float mineHandsY = 0.15f;
    private List<HandState> mineHands;
    void Start()
    {
        var leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        transform.localScale = new Vector3(Math.Abs(leftBottom.x), Math.Abs(leftBottom.y), transform.localScale.z);
        
        mineHands = new List<HandState>();
        var y = transform.position.y - transform.localScale.y + mineHandsY;
        var dx = transform.localScale.x * mineHandsWidth / (handStateCount - 1);
        var x = transform.position.x - dx * (handStateCount - 1) / 2;
        for (int i = 0; i < handStateCount; i++)
        {
            var p = new Vector3(x + dx * i, mineHandsY,-1.0f);
            var o = Instantiate(handPrefab, p, Quaternion.identity);
            var hs = o.GetComponent<HandState>();
            mineHands.Add(hs);
            hs.SetState(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
