using UnityEngine;

public enum TileFlags
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

    public TileFlags TryMoveToTile( AreaTile tileToMoveTo, out Character characterCollision )
    {
        characterCollision = null;

        if( !tileToMoveTo.IsWalkable )
        {
            return TileFlags.UNWALKABLE;

        }

        if( tileToMoveTo.OccupyingCharacter )
        {
            characterCollision = tileToMoveTo.OccupyingCharacter;

            return TileFlags.OCCUPIED;

        }

        // Swap tile's occupying character
        CurrentTile.OccupyingCharacter = null;
        tileToMoveTo.OccupyingCharacter = _character;

        transform.position = tileToMoveTo.WorldPosition;

        return TileFlags.WALKABLE;

    }

    public TileFlags TryMoveTowardsDirection( Vector2Int directionToMoveTowards, out Character characterCollision )
    {
        characterCollision = null;

        Vector2Int targetTilePosition = CurrentTile.LocalPosition + directionToMoveTowards;

        if( !_areaMap.GridMap.IsIndexInMap( targetTilePosition.x, targetTilePosition.y ) )
        {
            return TileFlags.UNWALKABLE;

        }

        AreaTile targetTile = _areaMap.GridMap[targetTilePosition.x, targetTilePosition.y];

        return TryMoveToTile( targetTile, out characterCollision );

    }

}
