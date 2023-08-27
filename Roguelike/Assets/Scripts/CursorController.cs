using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class CursorController : MonoBehaviour
{
    [SerializeField]
    private Camera _inputCamera;

    [SerializeField]
    private CursorGraphic _cursorGraphic;

    [SerializeField]
    private AreaMap _gridMap;

    [SerializeField]
    private Sprite _unselected;

    [SerializeField]
    private Sprite _highlight;

    private AreaTile _selectedTile;

    private Vector3 _mousePosition;

    protected virtual void Update()
    {
        _mousePosition = _inputCamera.ScreenToWorldPoint( Input.mousePosition );
        _mousePosition.z = -8.0f;

        _selectedTile = _gridMap.GridMap.WorldToTile( _mousePosition );

        if( _selectedTile == null )
        {
            return;

        }

        if( _selectedTile.OccupyingCharacter )
        {
            _cursorGraphic.Sprite = _highlight;
            transform.position = _selectedTile.WorldPosition;

            return;

        }

        transform.position = _mousePosition;
        _cursorGraphic.Sprite = _unselected;

    }

}
