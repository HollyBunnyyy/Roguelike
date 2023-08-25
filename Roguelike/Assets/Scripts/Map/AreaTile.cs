using System.Collections.Generic;
using UnityEngine;

public class AreaTile
{
    public bool IsWalkable = true;

    public List<AreaTile> Neighbors;

    public readonly Vector3     WorldPosition;
    public readonly Vector3Int  LocalPosition;

    public AreaTile( Vector3 worldPosition, Vector3Int localPosition )
    {
        this.WorldPosition = worldPosition;
        this.LocalPosition = localPosition;

    }

}
