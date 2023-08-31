using UnityEngine;

// Maybe consider delegate events?

public class CursorListener : MonoBehaviour
{
    public AreaTile CurrentSelectedTile => _gridMap.GridMap.WorldToTile( Roguelike.Instance.InputHandler.MousePositionWorld );

    public Entity CurrentSelectedEntity => CurrentSelectedTile?.OccupyingEntity;

    [SerializeField]
    private AreaMap _gridMap;

}
