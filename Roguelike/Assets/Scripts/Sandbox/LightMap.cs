using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LightMap : MonoBehaviour
{
    [SerializeField]
    private Tilemap _tileMap;

    private GridMap<LightNode> _lightMap;

    [SerializeField]
    private LightNode _lightNodePrefab;

    protected void Awake()
    {
        _tileMap.CompressBounds();

        _lightMap = new GridMap<LightNode>(
            _tileMap.origin.x,
            _tileMap.origin.y,
            _tileMap.size.x,
            _tileMap.size.y,
            _tileMap.cellSize,
            _tileMap.cellGap );

        for( int x = 0; x < _lightMap.Width; x++ )
        {
            for( int y = 0; y < _lightMap.Height; y++ )
            {
                LightNode node = Instantiate( _lightNodePrefab, transform );
                node.WorldPosition = _lightMap.TileToWorldPosition( x, y );
                node.LocalPosition = new Vector2Int( x, y );

                node.transform.position = node.WorldPosition;

                if( _tileMap.GetTile( new Vector3Int( x, y ) + _tileMap.origin ).name == "wall" )
                {
                    node.IsWall = true;
                    node.SpriteRenderer.color = Color.black;
                }

                _lightMap[x, y] = node;

            }
        }
    }

    protected void Start()
    {
        //Test( 24, 0, 16, Octant2D.OctantRegions[2] );

        foreach( LightNode node in GetTilesInColumn( 9, 24, 0, 0.8f, 0.4f, Octant2D.OctantRegions[2] ))
        {
            node.SpriteRenderer.color = Color.green;

        }

    }

    public void Test( int xIndex, int yIndex, int rowToScan, Octant2D octantRegion )
    {
        LightNode previousTile = null;



        for( int x = 0; x <= rowToScan; x++ )
        {
            for( int y = x; y >= 0; y-- )
            {
                int xNeighborIndex = xIndex + x * octantRegion.XX + y * octantRegion.XY;
                int yNeighborIndex = yIndex + x * octantRegion.YX + y * octantRegion.YY;

                float startSlope = 1.0f / ( ( x + _lightMap.CellCenter.x ) / ( y - _lightMap.CellCenter.y ) );
                float endSlope   = 1.0f / ( ( x - _lightMap.CellCenter.x ) / ( y + _lightMap.CellCenter.y ) );

                if( !_lightMap.IsIndexInMap( xNeighborIndex, yNeighborIndex ) )
                {
                    continue;
                }

                LightNode currentTile = _lightMap[xNeighborIndex, yNeighborIndex];
                currentTile.Show();

                if( previousTile != null )
                {
                    if( previousTile.IsWall && !currentTile.IsWall )
                    {
                        //Debug.DrawRay( _lightMap[xIndex, yIndex].WorldPosition, new Vector3( -16.0f * startSlope, 16.0f, 0.0f ), Color.green, 400.0f );

                        currentTile.SpriteRenderer.color = Color.green;

                    }

                    if( !previousTile.IsWall && currentTile.IsWall )
                    {
                        //Debug.DrawRay( _lightMap[xIndex, yIndex].WorldPosition, new Vector3( -16.0f * endSlope, 16.0f, 0.0f ), Color.red, 400.0f );

                        currentTile.SpriteRenderer.color = Color.red;

                    }
                }

                previousTile = currentTile;

            }
        }

    }

    public IEnumerable<LightNode> GetTilesInRow( int rowToCount, int startXIndex, int startYIndex, float startSlope, float endSlope, Octant2D octantRegion )
    {
        Debug.DrawRay( _lightMap[startXIndex, startYIndex].WorldPosition, new Vector3( -rowToCount * startSlope, rowToCount, 0.0f ), Color.green, 400.0f );
        Debug.DrawRay( _lightMap[startXIndex, startYIndex].WorldPosition, new Vector3( -rowToCount * endSlope, rowToCount, 0.0f ), Color.red, 400.0f );

        for( int i = rowToCount; i >= 0; i-- )
        {
            int xNeighborIndex = startXIndex + rowToCount * octantRegion.XX + i * octantRegion.XY;
            int yNeighborIndex = startYIndex + rowToCount * octantRegion.YX + i * octantRegion.YY;

            if( i - _lightMap.CellCenter.x >= ( rowToCount * startSlope ) )
                continue;

            if( i + _lightMap.CellCenter.x <= ( rowToCount * endSlope ) )
                continue;

            if( _lightMap.IsIndexInMap( xNeighborIndex, yNeighborIndex ) )
            {
                yield return _lightMap[xNeighborIndex, yNeighborIndex];
            }
        }
    }
}

/*
 * 
 * LightNode previousTile = null;

        float startSlope = 1.0f;
 * 
        for( int x = 0; x <= rowToScan; x++ )
        {
            for( int y = x; y >= 0; y-- )
            {
                int xNeighborIndex = xIndex + x * octantRegion.XX + y * octantRegion.XY;
                int yNeighborIndex = yIndex + x * octantRegion.YX + y * octantRegion.YY;

                float endSlope = ( x - _lightMap.CellCenter.x ) / ( y + _lightMap.CellCenter.y );

                if( !_lightMap.IsIndexInMap( xNeighborIndex, yNeighborIndex ) )
                {
                    continue;
                }

                LightNode currentTile = _lightMap[xNeighborIndex, yNeighborIndex];
                currentTile.Show();
                currentTile.Text.text = ( x + ", " + y ).ToString();

                if( previousTile != null )
                {
                    if( previousTile.IsWall && !currentTile.IsWall )
                    {
                        currentTile.SpriteRenderer.color = Color.green;

                        // slope should start
                    }

                    if( !previousTile.IsWall && currentTile.IsWall )
                    {
                        Debug.Log( endSlope + "," + x + ", " + y );

                        currentTile.SpriteRenderer.color = Color.red;

                        // slope should end
                    }
                }

                previousTile = currentTile;

            }
 */