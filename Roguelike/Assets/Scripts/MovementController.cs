using UnityEngine;

public enum TileFlag
{
    WALKABLE,
    UNWALKABLE,
    OCCUPIED

}

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private AreaMap _areaMap;

    [SerializeField]
    private Character _character;

    public AreaTile CurrentTile => _areaMap.GridMap.WorldToTile( transform.position );

    public TileFlag TryMoveToTile( AreaTile tileToMoveTo )
    {
        if( !tileToMoveTo.IsWalkable )
        {
            return TileFlag.UNWALKABLE;

        }

        if( tileToMoveTo.OccupyingCharacter )
        {
            return TileFlag.OCCUPIED;

        }

        // Swap tile's occupying character
        tileToMoveTo.OccupyingCharacter = _character;
        CurrentTile.OccupyingCharacter = null;

        transform.position = tileToMoveTo.WorldPosition;

        return TileFlag.WALKABLE;

    }

    public TileFlag TryMoveTowardsDirection( Vector2Int directionToMoveTowards )
    {
        Vector2Int targetTilePosition = CurrentTile.LocalPosition + directionToMoveTowards;

        if( !_areaMap.GridMap.IsIndexInMap( targetTilePosition.x, targetTilePosition.y ) )
        {
            return TileFlag.UNWALKABLE;

        }

        AreaTile targetTile = _areaMap.GridMap[targetTilePosition.x, targetTilePosition.y];

        return TryMoveToTile( targetTile );

    }

}
