using System.Collections;
using System.Collections.Generic;
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
            _tileMap.size.x, 
            _tileMap.size.y, 
            _tileMap.cellSize, 
            _tileMap.cellGap );

        for( int x = 0; x < GridMap.Width; x++ )
        {
            for( int y = 0; y < GridMap.Height; y++ )
            {
                Vector3 worldPosition = GridMap.TileToWorldPosition( x, y );
                Vector3Int localPosition = new Vector3Int( x, y );

                GridMap[x, y] = new AreaTile( worldPosition, localPosition )
                {
                    Neighbors = new List<AreaTile>( GridMap.GetSurroundingTiles( x, y, DirectionType.Ordinal ))

                };

            }
        }
    }
}
