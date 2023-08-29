using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( SpriteRenderer ) )]
public class CursorGraphic : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private Sprite _sprite;
    public Sprite Sprite 
    { 
        get { return _sprite; } 
        set { _spriteRenderer.sprite = _sprite = value; }

    }

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }


}
