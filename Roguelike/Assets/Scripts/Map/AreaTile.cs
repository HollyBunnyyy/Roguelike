using System.Collections.Generic;
using UnityEngine;

public class AreaTile
{
    public Character OccupyingCharacter;

    public bool IsWalkable = true;

    public List<AreaTile> Neighbors;

    public readonly Vector3     WorldPosition;
    public readonly Vector2Int  LocalPosition;

    public AreaTile( Vector3 worldPosition, Vector2Int localPosition )
    {
        this.WorldPosition = worldPosition;
        this.LocalPosition = localPosition;

    }

}
