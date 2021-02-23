using System.Collections.Generic;
using UnityEngine;

public class HandState : MonoBehaviour
{
    public List<Sprite> sprites;
    private int state;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetState(0);
    }

    public void SetState(int newState)
    {
        if (state == newState) return;
        if (sprites == null) return;
        if (newState < 0 || newState >= sprites.Count) return;
        spriteRenderer.sprite = sprites[state];
        state = newState;
    }
}
 