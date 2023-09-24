using System.Collections.Generic;
using UnityEngine;

// TODO : Make this code cleaner.

public class InventoryUIContainer : MonoBehaviour
{
    [SerializeField]
    private Inventory<ItemStack> _inventoryToView;

    [SerializeField]
    private InventoryUISlot _inventorySlotPrefab;

    [SerializeField]
    private RectTransform _contentArea;

    [SerializeField]
    private int _amountToPrewarm = 20;

    private List<InventoryUISlot> _inventorySlots = new List<InventoryUISlot>();

    protected virtual void Awake()
    {
        for( int i = 0; i < _amountToPrewarm; i++ )
        {
            AddSlot();

        }  
    }

    public void SetInventoryToView( Inventory<ItemStack> inventoryToWatch )
    {
        // Remove our event hook from the previous inventory.
        if( _inventoryToView != null )
        {
            _inventoryToView.OnCollectionDirty -= RedrawUI;

        }

        this._inventoryToView = inventoryToWatch;

        // Add our event hook to the new inventory.
        inventoryToWatch.OnCollectionDirty += RedrawUI;

        Enable();

        RedrawUI();

    }

    // UI speed will be increased greatly by not redrawing on EVERY IsDirty call from the inventory hook -
    // I'll add flags later to the actual Delegate event and have a little more control. Works for now!
    public void RedrawUI()
    {
        if( _inventorySlots.Count < _inventoryToView.MaxSize )
        {
            for( int i = 0; i < ( _inventoryToView.MaxSize - _inventorySlots.Count ); i++ )
            {
                AddSlot();

            }
        }

        for( int i = 0; i < _inventorySlots.Count; i++ )
        {
            InventoryUISlot inventorySlot = _inventorySlots[i];

            inventorySlot.ResetImage();
            inventorySlot._stackAmount.gameObject.SetActive( false );

            if( i >= _inventoryToView.MaxSize )
            {
                inventorySlot.gameObject.SetActive( false );

                continue;

            }

            inventorySlot.gameObject.SetActive( true );

            if( _inventoryToView.IsSlotIndexOccupied( i ) )
            {
                inventorySlot.TrySetImage( _inventoryToView[i].ItemID );

                if( _inventoryToView[i].Amount > 1 )
                {
                    inventorySlot._stackAmount.gameObject.SetActive( true );
                    inventorySlot._stackAmount.text = _inventoryToView[i].Amount.ToString();

                }

            }
        }
    }

    private void AddSlot()
    {
        InventoryUISlot inventorySlotToInstantiate = Instantiate( _inventorySlotPrefab, _contentArea );
        inventorySlotToInstantiate.gameObject.SetActive( false );

        _inventorySlots.Add( inventorySlotToInstantiate );

    }

    public void Enable()
    {
        gameObject.SetActive( true );

    }

    public void Disable()
    {
        gameObject.SetActive( false );

    }

}
