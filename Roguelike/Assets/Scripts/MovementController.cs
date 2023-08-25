using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private AreaMap _areaMap;

    [SerializeField]
    private Character _character;

    public AreaTile CurrentTile => _areaMap.GridMap.WorldToTile( transform.position );

    public bool TryMoveToTile( AreaTile tileToMoveTo )
    {
        if( tileToMoveTo.OccupyingCharacter || !tileToMoveTo.IsWalkable )
        {
            return false;

        }

        tileToMoveTo.OccupyingCharacter = _character;

        CurrentTile.OccupyingCharacter = null;

        Vector3 targetTilePosition = tileToMoveTo.WorldPosition;
        targetTilePosition.z = transform.position.z;

        transform.position = targetTilePosition;

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
