using UnityEngine;

public class DebugCharacter : Character
{
    [SerializeField]
    private int _maxInventorySize = 10;

    [SerializeField]
    private bool _shouldMove = true;

    private Direction2D _currentDirection = Direction2D.Right;

    protected virtual void Awake()
    {
        Inventory = new Inventory<Item>( _maxInventorySize );

        Inventory.TryAdd( 1, new Item( 120001 ) );

    }

    public override bool TurnAction()
    {
        if( _shouldMove )
        {
            TryMoveTowardsDirection( _currentDirection = Direction2D.Opposite( _currentDirection ), out Entity entityHit );


        }

        return true;

    }
}
