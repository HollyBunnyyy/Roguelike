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
                node.Hide();

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


        Test( 0, 12, 9 );

        //foreach( LightNode node in GetTilesInRow( 9, 24, 0, 0.8f, 0.4f, Octant2D.OctantRegions[2] ))
        //{
        //    node.SpriteRenderer.color = Color.green;
        //}
    }

    public void Test( int xIndex, int yIndex, int rowsToScan )
    {
        GetTilesInRow( rowsToScan, xIndex, yIndex, 1.0f, 0.0f, Octant2D.OctantRegions[7] );
    }

    public void GetTilesInRow( int rowToCount, int originXIndex, int originYIndex, float startSlope, float endSlope, Octant2D octantRegion )
    {
        int indexToScanFrom = Mathf.RoundToInt( rowToCount * startSlope );
        int indexToScanTo   = Mathf.FloorToInt( rowToCount * endSlope );

        Debug.DrawRay( _lightMap[originXIndex, originYIndex].WorldPosition, new Vector3( rowToCount * startSlope, -rowToCount, 0.0f ), Color.green, 400.0f );
        Debug.DrawRay( _lightMap[originXIndex, originYIndex].WorldPosition, new Vector3( rowToCount, rowToCount * endSlope, 0.0f ), Color.red, 400.0f );

        for( int i = indexToScanFrom; i >= indexToScanTo; i-- )
        {
            int xNeighborIndex = originXIndex + rowToCount * octantRegion.XX + i * octantRegion.XY;
            int yNeighborIndex = originYIndex + rowToCount * octantRegion.YX + i * octantRegion.YY;

            if( !_lightMap.IsIndexInMap( xNeighborIndex, yNeighborIndex ) )
            {
                continue;
            }

            _lightMap[xNeighborIndex, yNeighborIndex].Show();

        }

    }

    protected void OnDrawGizmos()
    {
        if( Application.isPlaying )
        {
            //Debug.DrawRay( _lightMap[24, 0].WorldPosition, new Vector3( ( 9.0f * _debugStartSlope ), 9.0f - _lightMap.CellCenter.y, 0.0f ), Color.cyan );
            //Debug.DrawRay( _lightMap[24, 0].WorldPosition, new Vector3( ( 9.0f * _debugEndSlope ) + _lightMap.CellCenter.x, 9.0f - _lightMap.CellCenter.y, 0.0f ), Color.cyan );

            //Debug.Log( Mathf.RoundToInt( 9.0f * _debugStartSlope ) + ", " + Mathf.FloorToInt( 9.0f * _debugEndSlope ));

        }
    }

}

/*
 * 
 * 
        for( int i = indexToScanFrom; i >= indexToScanTo; i-- )
        {
            int xNeighborIndex = originXIndex + rowToCount * octantRegion.XX + i * octantRegion.XY;
            int yNeighborIndex = originYIndex + rowToCount * octantRegion.YX + i * octantRegion.YY;

            if( !_lightMap.IsIndexInMap( xNeighborIndex, yNeighborIndex ) )
            {
                continue;
            }

            LightNode currentNode = _lightMap[xNeighborIndex, yNeighborIndex];
            currentNode.Show();

            if( previousNode != null )
            {
                if( previousNode.IsWall && !currentNode.IsWall )
                {
                    nextStartSlope = 1.0f / ( ( rowToCount + _lightMap.CellCenter.x ) / ( i + _lightMap.CellCenter.y ) );

                    Debug.DrawRay( _lightMap[originXIndex, originYIndex].WorldPosition, new Vector3( 16.0f * nextStartSlope, 16.0f, 0.0f ), Color.green, 400.0f );

                }

                if( !previousNode.IsWall && currentNode.IsWall )
                {
                    nextEndSlope = 1.0f / ( ( rowToCount - _lightMap.CellCenter.x ) / ( i + _lightMap.CellCenter.y ) );

                    Debug.DrawRay( _lightMap[originXIndex, originYIndex].WorldPosition, new Vector3( 16.0f * nextEndSlope, 16.0f, 0.0f ), Color.red, 400.0f );

                    GetTilesInRow( rowToCount + 1, originXIndex, originYIndex, nextStartSlope, nextEndSlope, octantRegion );

                }
            }

            previousNode = currentNode;

        }

        if( rowToCount > 16 )
        {
            return;
        }

        GetTilesInRow( rowToCount + 1, originXIndex, originYIndex, nextStartSlope, nextEndSlope, octantRegion );

 * 
 * 
 * 
 * 
 * 
 * 
 * 
 public void Test( int xIndex, int yIndex, int rowToScan, Octant2D octantRegion )
    {
        LightNode previousTile = null;



        for( int i = 0; i <= rowToScan; x++ )
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


    public IEnumerable<LightNode> GetTilesInRow( int rowToCount, int originXIndex, int originYIndex, float startSlope, float endSlope, Octant2D octantRegion )
    {
        //Debug.DrawRay( _lightMap[startXIndex, startYIndex].WorldPosition, new Vector3( -rowToCount * startSlope, rowToCount, 0.0f ), Color.green, 400.0f );
        //Debug.DrawRay( _lightMap[startXIndex, startYIndex].WorldPosition, new Vector3( -rowToCount * endSlope, rowToCount, 0.0f ), Color.red, 400.0f );

        int indexToScanFrom = Mathf.RoundToInt( rowToCount * startSlope );
        int indexToScanTo   = Mathf.CeilToInt( rowToCount * endSlope );

        for( int i = indexToScanFrom; i >= indexToScanTo; i-- )
        {
            int xNeighborIndex = originXIndex + rowToCount * octantRegion.XX + i * octantRegion.XY;
            int yNeighborIndex = originYIndex + rowToCount * octantRegion.YX + i * octantRegion.YY;

            if( _lightMap.IsIndexInMap( xNeighborIndex, yNeighborIndex ) )
            {
                yield return _lightMap[xNeighborIndex, yNeighborIndex];
            }
        }
    }

 */