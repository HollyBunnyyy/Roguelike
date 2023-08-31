using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private InputHandler _inputHandler;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    protected override void Start()
    {
        base.Start();

        Inventory.IncreaseTotalSize( 4 );

        Inventory.TryAdd( 0, new Item( 120001 ) );
        Inventory.TryAdd( 1, new Item( 120002 ) );
        Inventory.TryAdd( 6, new Item( 120003 ) );
        Inventory.TryAdd( 12, new Item( 120003 ) );
        Inventory.TryAdd( 13, new Item( 120002 ) );

    }

    public override bool TurnAction()
    {
        if( Input.anyKeyDown )
        {
            if( _inputHandler.WASDAxis != Vector2.zero )
            {
                TryMoveTowardsDirection( Vector2Int.RoundToInt( _inputHandler.WASDAxis ), out Entity entityHit );

                if( entityHit is Character )
                {
                    ( entityHit as Character ).Damage( 5.0f );

                }

                return true;

            }

            if( Input.GetKeyDown( KeyCode.Space ) )
            {
                // skip turn

                return true;

            }

        }

        return false;

    }



}
