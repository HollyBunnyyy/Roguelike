using UnityEngine;

[RequireComponent( typeof( SpriteRenderer ) )]
public class Entity : MovementController
{
    [SerializeField]
    private int id = 0;
    public int ID => id;

    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer 
    {
        get { return _spriteRenderer != null ? _spriteRenderer : _spriteRenderer = GetComponent<SpriteRenderer>(); }
    }

    public void SetEntityID( int id )
    {
        this.id = id;
    }

    public void SetEntitySprite( Sprite sprite )
    {
        SpriteRenderer.sprite = sprite;
    }
}
