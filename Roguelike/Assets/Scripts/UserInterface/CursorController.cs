using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : CursorListener
{
    [SerializeField]
    private InventoryUIContainer _inventoryUIContainer;

    [SerializeField]
    private CursorGraphicHandler _cursorGraphicHandler;

    [SerializeField]
    private Sprite _selectionSprite;

    [SerializeField]
    private Sprite _unselectedSprite;

    protected void Update()
    {
        if( CurrentSelectedEntity is Character )
        {
            _inventoryUIContainer.SetInventoryToView( ( CurrentSelectedEntity as Character ).Inventory );

        }

    }

}
