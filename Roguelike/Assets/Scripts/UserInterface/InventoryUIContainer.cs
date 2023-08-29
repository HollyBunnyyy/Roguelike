using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : implement object pooling.

public class InventoryUIContainer : MonoBehaviour
{
    [SerializeField]
    private Character _character;

    [SerializeField]
    private InventoryUISlot _inventorySlotPrefab;

    [SerializeField]
    private RectTransform _contentArea;

    private List<InventoryUISlot> _inventorySlots = new List<InventoryUISlot>();

    protected virtual void Awake()
    {
        for( int i = 0; i < 20; i++ )
        {
            InventoryUISlot inventorySlotToInstantiate = Instantiate( _inventorySlotPrefab, _contentArea );
            inventorySlotToInstantiate.gameObject.SetActive( false );
            
            _inventorySlots.Add( inventorySlotToInstantiate );

        }  
    }

    protected virtual void Start()
    {
        SetCharacterInventoryToWatch( _character );

    }

    public void SetCharacterInventoryToWatch( Character characterToWatch )
    {
        // Remove our event hook from the previous character's inventory.
        _character.Inventory.OnCollectionDirty -= RedrawUI;

        this._character = characterToWatch;

        // Add our event hook to the new character's inventory.
        characterToWatch.Inventory.OnCollectionDirty += RedrawUI;

    }

    public void RedrawUI()
    {
        foreach( InventoryUISlot slot in _inventorySlots )
        {
            slot.ResetImage();
            slot.gameObject.SetActive( false );

        }

        for( int i = 0; i < _character.Inventory.MaxSize; i++ )
        {
            _inventorySlots[i].gameObject.SetActive( true );

            if( !_character.Inventory.IsSlotIndexOccupied( i ) )
            {
                continue;

            }

            _inventorySlots[i].TrySetImage( _character.Inventory[i].ID );

        }
    }
}
