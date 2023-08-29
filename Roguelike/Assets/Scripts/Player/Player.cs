using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private InventoryUIContainer _inventoryUI;

    [SerializeField]
    private InputHandler _inputHandler;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Character _testCharacter;

    [SerializeField]
    private InventoryUIContainer _inventoryContainer;

    private Character characterToWatch;

    protected override void Start()
    {
        base.Start();

        Inventory.IncreaseTotalSize( 4 );

        Inventory.TryAdd( 0, new Item( 120001 ) );
        Inventory.TryAdd( 1, new Item( 120002 ) );
        Inventory.TryAdd( 6, new Item( 120003 ) );

        foreach( int slot in Inventory.GetUnoccupiedSlots() )
        {
            Debug.Log( slot );

        }

        Character characterToWatch = this;

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

                characterToWatch = ( characterToWatch == _testCharacter ) ? this : _testCharacter;

                _inventoryContainer.SetCharacterInventoryToWatch( characterToWatch );

                return true;

            }

        }

        return false;

    }



}
