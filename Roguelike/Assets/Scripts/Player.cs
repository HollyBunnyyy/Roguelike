using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private InputHandler _inputHandler;

    public override bool TurnAction()
    {
        if( Input.anyKeyDown )
        {
            if( _inputHandler.WASDAxis != Vector2.zero )
            {
                TileFlag tileFlag = TryMoveTowardsDirection( Vector2Int.RoundToInt( _inputHandler.WASDAxis ) );

                Debug.Log( tileFlag );

                return true;

            }

            if( Input.GetKeyDown( KeyCode.Space ) )
            {
                Inventory.DecreaseTotalSize( 1 );

                // skip turn

                return true;

            }

        }

        return false;

    }



}
