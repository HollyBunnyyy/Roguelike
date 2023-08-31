using UnityEngine;

[RequireComponent( typeof( SpriteRenderer ) )]
public class CursorGraphicHandler : MonoBehaviour
{
    public Sprite Sprite => _spriteRenderer.sprite;

    private SpriteRenderer _spriteRenderer;

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void SetCursorSprite( Sprite spriteToSet )
    {
        _spriteRenderer.sprite = spriteToSet;

    }


}
