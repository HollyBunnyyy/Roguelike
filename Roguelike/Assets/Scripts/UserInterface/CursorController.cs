using UnityEngine;
using TMPro;

public class CursorController : MonoBehaviour
{
    [SerializeField]
    private InventoryUIContainer _inventoryUI;

    [SerializeField]
    private TMP_Text _text;

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

    private Entity _currentEntity;

    protected virtual void Update()
    {
        _mousePosition = _inputCamera.ScreenToWorldPoint( Input.mousePosition );
        _mousePosition.z = -8.0f;

        _selectedTile = _gridMap.GridMap.WorldToTile( _mousePosition );

        if( _selectedTile == null )
        {
            return;

        }

        _currentEntity = _selectedTile.OccupyingEntity;

        if( _currentEntity )
        {
            _cursorGraphic.Sprite = _highlight;
            transform.position = _selectedTile.WorldPosition;

            if( _currentEntity is Character )
            {
                _inventoryUI.SetInventoryToView( ( _currentEntity as Character ).Inventory );

            }

            return;

        }

        transform.position = _mousePosition;
        _cursorGraphic.Sprite = _unselected;

    }

}
