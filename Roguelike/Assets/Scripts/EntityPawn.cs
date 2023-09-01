using UnityEngine;

public class EntityPawn : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private EntityPawnPool _entityPawnPool;

    public void SetEntityToDisplay( Sprite sprite )
    {
        Debug.Log( sprite );

        _spriteRenderer.sprite = sprite;

    }

    public bool TryBindToPool( EntityPawnPool entityPoolToBind )
    {
        if( _entityPawnPool != null )
        {
            return false;

        }

        _entityPawnPool = entityPoolToBind;

        return true;

    }

    public void Disable()
    {
        _spriteRenderer.sprite = null;

        _entityPawnPool.ReturnToPool( this );

        gameObject.SetActive( false );
    }

    public void Enable()
    {
        gameObject.SetActive( true );
    }

}
