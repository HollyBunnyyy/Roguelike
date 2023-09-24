using UnityEngine;

// Maybe consider delegate events?

public class CursorListener : MonoBehaviour
{
    public AreaTile CurrentSelectedTile => CurrentMap.GridMap.WorldToTile( transform.position);

    public Entity CurrentSelectedEntity => CurrentSelectedTile?.OccupyingEntity;

    public AreaMap CurrentMap;

}
