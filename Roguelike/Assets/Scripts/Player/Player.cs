using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Player : Character
{
    [SerializeField]
    private InventoryUIContainer _inventoryUI;

    [SerializeField]
    private InputHandler _inputHandler;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    protected override void Start()
    {
        base.Start();

        Inventory.TryAdd( 0, new Item( 120001 ) );
        Inventory.TryAdd( 1, new Item( 120002 ) );
        Inventory.TryAdd( 6, new Item( 120003 ) );


    }

    public override bool TurnAction()
    {
        if( Input.anyKeyDown )
        {
            if( _inputHandler.WASDAxis != Vector2.zero )
            {
                TryMoveTowardsDirection( Vector2Int.RoundToInt( _inputHandler.WASDAxis ), out Character character );

                if( character )
                {
                    character.Damage( 5.0f );

                }

                return true;

            }

            if( Input.GetKeyDown( KeyCode.Space ) )
            {
                // skip turn

                foreach( Item item in Inventory.DecreaseTotalSize( 2 ) )
                {
                    Debug.Log( item.ID );

                }
                
                return true;

            }

        }

        return false;

    }



}
