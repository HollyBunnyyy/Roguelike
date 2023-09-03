using UnityEngine;

public class Player : Character, ITurnAgent
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    protected void Start()
    {
        if( !TryMoveToTile( CurrentTile, out Entity entityOccupying ) )
        {
            Debug.LogError( "Attempt to set entities current tile failed." );

        }

        Inventory.TryAddNext( new Item( 120001 ) );
        Inventory.TryAddNext( new Item( 120002 ) );
        Inventory.TryAddNext( new Item( 120003 ) );

        Roguelike.Instance.GameManager.TurnHandler.AddAgent( this );

        Roguelike.Instance.AssetManager.TryGetMetaData( 0, out CharacterMetaData characterData );

        _spriteRenderer.sprite = characterData.Sprite;

    }

    public bool TurnAction()
    {
        if( Input.anyKeyDown )
        {
            if( Roguelike.Instance.InputHandler.WASDAxis != Vector2.zero )
            {
                TryMoveTowardsDirection( Vector2Int.RoundToInt( Roguelike.Instance.InputHandler.WASDAxis ), out Entity entityHit );

                if( entityHit is Character )
                {
                    ( entityHit as Character ).Damage( 5.0f );

                }

                return true;

            }

            if( Input.GetKeyDown( KeyCode.Space ) )
            {
                // skip turn

                if( CurrentTile.OccupyingItems.OccupiedCount != 0 )
                {
                    CurrentTile.OccupyingItems.TryRemove( 0, out Item itemRemoved );

                    Inventory.TryAddNext( itemRemoved );

                }


                return true;

            }

        }

        return false;

    }



}
