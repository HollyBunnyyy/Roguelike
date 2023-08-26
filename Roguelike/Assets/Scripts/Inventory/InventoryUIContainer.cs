using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : implement object pooling. Heavy WIP for now.

public class InventoryUIContainer : MonoBehaviour
{
    [SerializeField]
    private Character _character;

    [SerializeField]
    private InventoryUISlot _inventorySlotPrefab;

    [SerializeField]
    private RectTransform _contentArea;

    private List<InventoryUISlot> _inventorySlots = new List<InventoryUISlot>();

    private int _previousInventoryMaxSize;

    private bool _isInventoryDirty => _previousInventoryMaxSize != _character.Inventory.MaxSize;

    protected virtual void Awake()
    {
        for( int i = 0; i < 20; i++ )
        {
            InventoryUISlot inventorySlotToInstantiate = Instantiate( _inventorySlotPrefab, _contentArea );
            inventorySlotToInstantiate.gameObject.SetActive( false );
            
            _inventorySlots.Add( inventorySlotToInstantiate );

        }  
    }

    protected virtual void Update()
    {
        if( _isInventoryDirty )
        {
            _previousInventoryMaxSize = _character.Inventory.MaxSize;

            foreach( InventoryUISlot slot in _inventorySlots )
            {
                slot.gameObject.SetActive( false );

            }

            for( int i = 0; i < _character.Inventory.MaxSize; i++ )
            {
                _inventorySlots[i].gameObject.SetActive( true );

            }

        }

    }


}
