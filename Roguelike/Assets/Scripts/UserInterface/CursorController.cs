using UnityEngine;
using TMPro;

public delegate AreaTile CursorHoverHandler();

public class CursorController : MonoBehaviour
{
    public event CursorHoverHandler OnCursorHover;

    [SerializeField]
    private InventoryUIContainer _inventoryUI;

    [SerializeField]
    private Camera _inputCamera;

    [SerializeField]
    private AreaMap _gridMap;

    private Vector3 _mousePosition;

    private AreaTile _selectedTile;

    private Entity _currentEntity;

    protected virtual void Update()
    {
        _mousePosition = _inputCamera.ScreenToWorldPoint( Input.mousePosition );
        _mousePosition.z = transform.position.z;

        _selectedTile = _gridMap.GridMap.WorldToTile( _mousePosition );

        if( _selectedTile == null )
        {
            return;

        }

        _currentEntity = _selectedTile.OccupyingEntity;

        if( _currentEntity )
        {
            transform.position = _selectedTile.WorldPosition;

        }

        transform.position = _mousePosition;

    }

}
