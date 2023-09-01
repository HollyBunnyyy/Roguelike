using UnityEngine;
using UnityEngine.Tilemaps;

public class AreaMap : MonoBehaviour
{
    [SerializeField]
    private Tilemap _tileMap;

    public GridMap<AreaTile> GridMap;

    protected virtual void Awake()
    {
        _tileMap.CompressBounds();

        GridMap = new GridMap<AreaTile>( 
            _tileMap.origin.x,
            _tileMap.origin.y,
            _tileMap.size.x, 
            _tileMap.size.y, 
            _tileMap.cellSize, 
            _tileMap.cellGap );

        for( int x = 0; x < GridMap.Width; x++ )
        {
            for( int y = 0; y < GridMap.Height; y++ )
            {
                Vector3 worldPosition = GridMap.TileToWorldPosition( x, y );
                Vector2Int localPosition = new Vector2Int( x, y );

                GridMap[x, y] = new AreaTile( worldPosition, localPosition );

                if( x == 5 && y == 5 )
                {
                    GridMap[x, y].OccupyingItems.TryAdd( 0, new Item( 120003 ) );

                }

                if( !_tileMap.GetTile( new Vector3Int( x, y ) + _tileMap.origin  ))
                {
                    GridMap[x, y].IsWalkable = false;
                }

            }
        }
    }
}
