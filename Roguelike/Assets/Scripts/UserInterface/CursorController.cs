using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : CursorListener
{
    [SerializeField]
    private CursorGraphicHandler _cursorGraphicHandler;

    [SerializeField]
    private Sprite _selectionSprite;

    [SerializeField]
    private Sprite _unselectedSprite;

    protected void LateUpdate()
    {
        if( CurrentSelectedEntity is Character )
        {
            _cursorGraphicHandler.SetCursorSprite( _selectionSprite );

            return;

        }

        _cursorGraphicHandler.SetCursorSprite( _unselectedSprite );

    }

    public void MoveTowardsDirection( Vector2Int directionToMoveTowards )
    {
        Vector2Int targetTilePosition = CurrentSelectedTile.LocalPosition + directionToMoveTowards;

        if( !CurrentMap.GridMap.IsIndexInMap( targetTilePosition.x, targetTilePosition.y )  )
        {
            return;
        }

        transform.position = CurrentMap.GridMap[targetTilePosition.x, targetTilePosition.y].WorldPosition;

    }
}

/*
 * 
 *         if( CurrentSelectedEntity is Character )
        {
            _cursorGraphicHandler.transform.position = CurrentSelectedTile.WorldPosition;
            _cursorGraphicHandler.SetCursorSprite( _selectionSprite );

            if( CurrentSelectedEntity != _previousCharacter )
            {
                _previousCharacter = CurrentSelectedEntity;

                _inventoryUIContainer.SetInventoryToView( ( CurrentSelectedEntity as Character ).Inventory );

            }

            //_inventoryUIContainer.Enable();

            return;

        }

        _cursorGraphicHandler.SetCursorSprite( _unselectedSprite );
        //_inventoryUIContainer.Disable();
 * 
 * 
 * 
 * 
 */