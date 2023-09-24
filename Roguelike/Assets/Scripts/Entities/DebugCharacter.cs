using UnityEngine;

public class DebugCharacter : Character, ITurnAgent
{
    [SerializeField]
    private int _maxInventorySize = 10;

    [SerializeField]
    private bool _shouldMove = true;

    private Direction2D _currentDirection = Direction2D.Right;

    protected void Start()
    {
        if( !TryMoveToTile( CurrentTile, out Entity entityOccupying ) )
        {
            Debug.LogError( "Attempt to set entities current tile failed." );

        }


        Inventory = new Inventory<ItemStack>( _maxInventorySize );

        Inventory.TryAddNext( new ItemStack( 120001, 1 ) );

        Roguelike.Instance.GameManager.TurnHandler.AddAgent( this );

    }

    public bool TurnAction()
    {
        if( _shouldMove )
        {
            TryMoveTowardsDirection( _currentDirection = Direction2D.Opposite( _currentDirection ), out Entity entityHit );
        }

        return true;

    }
}
