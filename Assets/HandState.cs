using System.Collections.Generic;
using UnityEngine;

public class HandState : MonoBehaviour
{
    public List<Sprite> sprites;
    private int state = -1;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetState(int newState)
    {
        if (state == newState) return;
        if (sprites == null) return;
        if (newState < 0 || newState >= sprites.Count) return;
        spriteRenderer.sprite = sprites[newState];
        state = newState;
    }
}
 