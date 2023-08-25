using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private AreaMap _areaMap;

    public AreaTile CurrentTile => _areaMap.GridMap.WorldToTile( transform.position );

    public bool TryMoveToTile( AreaTile tileToMoveTo )
    {
        if( tileToMoveTo.OccupyingCharacter || !tileToMoveTo.IsWalkable )
        {
            return false;

        }

        tileToMoveTo.OccupyingCharacter = CurrentTile.OccupyingCharacter;

        CurrentTile.OccupyingCharacter = null;

        transform.position = tileToMoveTo.WorldPosition;

        return true;

    }


    public bool TryMoveTowardsDirection( Vector2Int directionToMoveTowards )
    {
        Vector2Int targetTilePosition = CurrentTile.LocalPosition + directionToMoveTowards;

        if( !_areaMap.GridMap.IsIndexInMap( targetTilePosition.x, targetTilePosition.y ) )
        {
            return false;

        }

        AreaTile targetTile = _areaMap.GridMap[targetTilePosition.x, targetTilePosition.y];

        return TryMoveToTile( targetTile );

    }

}
