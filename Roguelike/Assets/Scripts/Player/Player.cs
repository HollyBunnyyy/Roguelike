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

        Roguelike.Instance.GameManager.TurnHandler.AddAgent( this );

    }

    public bool TurnAction()
    {
        if( Input.anyKeyDown )
        {
            if( Roguelike.Instance.InputHandler.WASDAxis != Vector2.zero )
            {
                TryMoveTowardsDirection( Vector2Int.RoundToInt( Roguelike.Instance.InputHandler.WASDAxis ), out Entity entityHit );

                if( CurrentTile.OccupyingItems.OccupiedCount > 0 )
                {
                    Debug.Log( CurrentTile.OccupyingItems.GetEarliestItem() );

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

                    Inventory.TryAddNext( itemRemoved );

                }


                return true;

            }

        }

        return false;

    }



}
