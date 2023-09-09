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

    private Entity _previousCharacter;

    protected void LateUpdate()
    {
        if( CurrentSelectedEntity is Character )
        {
            _cursorGraphicHandler.transform.position = CurrentSelectedTile.WorldPosition;
            _cursorGraphicHandler.SetCursorSprite( _selectionSprite );

            if( CurrentSelectedEntity != _previousCharacter )
            {
                _previousCharacter = CurrentSelectedEntity;

                _inventoryUIContainer.SetInventoryToView( ( CurrentSelectedEntity as Character ).Inventory );

            }

            _inventoryUIContainer.Enable();

            return;

        }

        _cursorGraphicHandler.SetCursorSprite( _unselectedSprite );
        _inventoryUIContainer.Disable();

    }

}
