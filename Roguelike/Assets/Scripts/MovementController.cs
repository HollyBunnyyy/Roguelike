using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private AreaMap _areaMap;

    [SerializeField]
    private Entity _entityToMove;

    public AreaTile CurrentTile => _areaMap.GridMap.WorldToTile( transform.position );

    protected virtual void Start()
    {
        TryMoveToTile( CurrentTile, out Entity entityOccupying );

    }

    public bool TryMoveToTile( AreaTile tileToMoveTo, out Entity entityOccupying )
    {
        entityOccupying = tileToMoveTo.OccupyingEntity;

        if( !tileToMoveTo.IsWalkable || entityOccupying )
        {
            return false;

        }

        // Swap tile's occupying character
        CurrentTile.OccupyingEntity = null;
        tileToMoveTo.OccupyingEntity = _entityToMove;

        transform.position = tileToMoveTo.WorldPosition;

        return true;

    }

    public bool TryMoveTowardsDirection( Vector2Int directionToMoveTowards, out Entity entityOccupying )
    {
        entityOccupying = null;

        Vector2Int targetTilePosition = CurrentTile.LocalPosition + directionToMoveTowards;

        if( !_areaMap.GridMap.IsIndexInMap( targetTilePosition.x, targetTilePosition.y ) )
        {
            return false;

        }

        AreaTile targetTile = _areaMap.GridMap[targetTilePosition.x, targetTilePosition.y];

        return TryMoveToTile( targetTile, out entityOccupying );

    }

}
