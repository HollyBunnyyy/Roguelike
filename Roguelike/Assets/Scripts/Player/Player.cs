using UnityEngine;

public class Player : Character, ITurnAgent
{
    [SerializeField]
    private CursorController _cursorController;

    private bool _isMovingCursor = false;

    protected void Start()
    {
        if( !TryMoveToTile( CurrentTile, out Entity entityOccupying ) )
        {
            Debug.LogError( "Attempt to set entities current tile failed." );

        }

        Inventory.TryAddNext( new ItemStack( 120001, 30 ) );
        Inventory.TryAddNext( new ItemStack( 120002, 12 ) );
        Inventory.TryAddNext( new ItemStack( 120003, 1 ) );

        Roguelike.Instance.GameManager.TurnHandler.AddAgent( this );

        Roguelike.Instance.AssetManager.TryGetMetaData( 1, out CharacterMetaData characterData );

        SetEntitySprite( characterData.Sprite );

    }

    public bool TurnAction()
    {
        if( Input.anyKeyDown )
        {
            if( Input.GetKeyDown( KeyCode.Space ) )
            {
                _isMovingCursor = !_isMovingCursor;

                _cursorController.transform.position = transform.position;
                _cursorController.gameObject.SetActive( false );

                if( _cursorController.CurrentSelectedEntity as Character )
                {
                    Roguelike.Instance.UIManager.EventLog.WriteTextToLog( _cursorController.CurrentSelectedEntity.ID.ToString() );

                }

            }

            if( _isMovingCursor )
            {
                _cursorController.MoveTowardsDirection( Vector2Int.RoundToInt( Roguelike.Instance.InputHandler.WASDAxis ) );

                _cursorController.gameObject.SetActive( true );

                return false;

            }

            if( Roguelike.Instance.InputHandler.WASDAxis != Vector2.zero )
            {
                TryMoveTowardsDirection( Vector2Int.RoundToInt( Roguelike.Instance.InputHandler.WASDAxis ), out Entity entityHit );

                if( entityHit is Character )
                {
                    ( entityHit as Character ).Health.Damage( 5.0f );

                }

                return true;

            }

            if( Input.GetKeyDown( KeyCode.Space ) )
            {
                // skip turn

                if( CurrentTile.OccupyingItems.OccupiedCount != 0 )
                {
                    CurrentTile.OccupyingItems.TryRemove( 0, out ItemStack itemRemoved );

                    Inventory.TryAddNext( itemRemoved );

                }


                return true;

            }

        }

        return false;

    }



}
