using UnityEngine;

public class AreaTile
{
    public bool IsWalkable = true;

    public Entity OccupyingEntity;

    public readonly Vector3     WorldPosition;
    public readonly Vector2Int  LocalPosition;

    public AreaTile( Vector3 worldPosition, Vector2Int localPosition )
    {
        this.WorldPosition = worldPosition;
        this.LocalPosition = localPosition;

    }

}
