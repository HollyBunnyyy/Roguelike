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

                Debug.Log( Inventory.TryAdd( 0, new Item( 0 ) ) );

                _inventoryUI.RedrawUI();

                return true;

            }

        }

        return false;

    }



}