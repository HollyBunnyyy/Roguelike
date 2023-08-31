using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField]
    private AreaMap _gridMap;

    private AreaTile _currentSelectedTile;

    public AreaTile CurrentSelectedTile => _currentSelectedTile;
    public Entity CurrentSelectedEntity => _currentSelectedTile.OccupyingEntity;

    protected virtual void Update()
    {
        _currentSelectedTile = _gridMap.GridMap.WorldToTile( Roguelike.Instance.InputHandler.MousePositionWorld );

    }
}
