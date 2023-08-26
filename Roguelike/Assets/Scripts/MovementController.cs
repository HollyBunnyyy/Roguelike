using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private AreaMap _areaMap;

    [SerializeField]
    private Character _character;

    public AreaTile CurrentTile => _areaMap.GridMap.WorldToTile( transform.position );

    public bool TryMoveToTile( AreaTile tileToMoveTo, out Character characterCollision )
    {
        characterCollision = tileToMoveTo.OccupyingCharacter;

        if( !tileToMoveTo.IsWalkable )
        {
            return false;

        }

        if( tileToMoveTo.OccupyingCharacter )
        {
            return false;

        }

        // Swap tile's occupying character
        CurrentTile.OccupyingCharacter = null;
        tileToMoveTo.OccupyingCharacter = _character;

        transform.position = tileToMoveTo.WorldPosition;

        return true;

    }

    public bool TryMoveTowardsDirection( Vector2Int directionToMoveTowards, out Character characterCollision )
    {
        characterCollision = null;

        Vector2Int targetTilePosition = CurrentTile.LocalPosition + directionToMoveTowards;

        if( !_areaMap.GridMap.IsIndexInMap( targetTilePosition.x, targetTilePosition.y ) )
        {
            return false;

        }

        AreaTile targetTile = _areaMap.GridMap[targetTilePosition.x, targetTilePosition.y];

        return TryMoveToTile( targetTile, out characterCollision );

    }

}
