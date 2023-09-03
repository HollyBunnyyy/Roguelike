using UnityEngine;

// TODO : Add logic to set the sprite of the gameobject when the ID is changed.

[RequireComponent( typeof( SpriteRenderer ) )]
public class Entity : MovementController
{
    public int ID;

    [HideInInspector]
    public SpriteRenderer SpriteRenderer;

    private EntityPool _entityPool;

    protected void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool TryBindToPool( EntityPool entityPoolToBind )
    {
        if( _entityPool != null )
        {
            return false;
        }

        _entityPool = entityPoolToBind;

        return true;

    }

    public void Disable()
    {
        SpriteRenderer.sprite = null;

        _entityPool.ReturnToPool( this );

        gameObject.SetActive( false );
    }

    public void Enable()
    {
        gameObject.SetActive( true );
    }

}
