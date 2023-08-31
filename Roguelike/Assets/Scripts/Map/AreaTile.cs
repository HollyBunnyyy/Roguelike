using UnityEngine;

// TODO : add a way to show items on the ground - store them ( Inventory ) and allow the player to interact with them.

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
