using UnityEngine;

[RequireComponent( typeof( SpriteRenderer ) )]
public class Entity : MovementController
{
    [SerializeField]
    private int id = 0;
    public int ID => id;

    private SpriteRenderer _spriteRenderer;

    protected void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SetEntityID( ID );

    }

    public void SetEntityID( int id )
    {
        this.id = id;

        if( Roguelike.Instance.AssetManager.TryGetMetaData( id, out CharacterMetaData characterData ) )
        {
            SetEntitySprite( characterData.Sprite );
        }
    }

    public void SetEntitySprite( Sprite sprite )
    {
        _spriteRenderer.sprite = sprite;
    }
}
