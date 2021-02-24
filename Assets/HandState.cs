using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandState : MonoBehaviour
{
    public List<Sprite> sprites;
    private int state = -1;
    private SpriteRenderer spriteRenderer;
    private TMP_Text nick;
    private TMP_Text score;
    private Collider2D _collider2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        nick = GetComponentInChildren<TMP_Text>();
        score = GetComponentInChildren<TMP_Text>();
        _collider2D = GetComponent<Collider2D>();
    }

    public void SetNick(string text)
    {
        nick.SetText(text);
    }
    
    public void SetScore(string text)
    {
        score.SetText(text);
    }

    public bool isPointed(ref Vector2 point)
    {
        return _collider2D.OverlapPoint(point);
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
 