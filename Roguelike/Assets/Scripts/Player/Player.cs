using UnityEngine;

public class Player : Character, ITurnAgent
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    protected void Awake()
    {
        Inventory.IncreaseTotalSize( 4 );

        Inventory.TryAdd( 0, new Item( 120001 ) );
        Inventory.TryAdd( 1, new Item( 120002 ) );
        Inventory.TryAdd( 6, new Item( 120003 ) );
        Inventory.TryAdd( 12, new Item( 120003 ) );
        Inventory.TryAdd( 13, new Item( 120002 ) );

        Roguelike.Instance.GameManager.TurnHandler.AddAgent( this );

    }

    public bool TurnAction()
    {
        if( Input.anyKeyDown )
        {
            if( Roguelike.Instance.InputHandler.WASDAxis != Vector2.zero )
            {
                TryMoveTowardsDirection( Vector2Int.RoundToInt( Roguelike.Instance.InputHandler.WASDAxis ), out Entity entityHit );

                if( CurrentTile.OccupyingItems.OccupiedCount != 0 )
                {
                    Debug.Log( CurrentTile.OccupyingItems[0].ID );

                }

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

                    Inventory.TryAdd( 2, itemRemoved );

                }


                return true;

            }

        }

        return false;

    }



}
