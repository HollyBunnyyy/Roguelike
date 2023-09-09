using System.Collections.Generic;
using UnityEngine;

public enum RadiusShape
{
    SQUARE,
    DIAMOND,
    CIRCLE

}

public class GridMap<T>
{
    public readonly int Width;
    public readonly int Height;

    public readonly Vector2 CellGap;
    public readonly Vector2 CellSize;
    public readonly Vector2 CellArea;
    public readonly Vector2 CellCenter;
    public readonly Vector2 Origin;

    public int Size => Width * Height;

    private T[,] _tileGrid;

    public T this[int x, int y]
    {
        get => _tileGrid[x, y];
        set => _tileGrid[x, y] = value;

    }

    public GridMap( float startX, float startY, int width, int height, Vector2 cellSize, Vector2 cellGap )
    {
        this.Width      = width;
        this.Height     = height;
        this.CellGap    = cellGap;
        this.CellSize   = cellSize;
        this.CellArea   = CellSize + CellGap;
        this.CellCenter = CellArea / 2.0f;
        this.Origin     = new Vector2( startX, startY ) + CellCenter;

        this._tileGrid = new T[width, height];

        for( int x = 0; x < width; x++ )
        {
            for( int y = 0; y < height; y++ )
            {
                _tileGrid[x, y] = default;

            }
        }
    }

    /// <summary>
    /// Returns whether the given index is in the grid.
    /// </summary>
    public bool IsIndexInMap( int xIndex, int yIndex )
    {
        return xIndex >= 0 && yIndex >= 0 && xIndex < Width && yIndex < Height;

    }

    /// <summary>
    /// Converts a worldspace position to tilespace coordinates.
    /// </summary>
    public Vector2Int WorldToTilePosition( Vector3 worldPosition )
    {
        Vector2Int tilePositionOnGrid = new Vector2Int()
        {
            x = Mathf.RoundToInt( ( worldPosition.x - Origin.x ) / CellArea.x ),
            y = Mathf.RoundToInt( ( worldPosition.y - Origin.y ) / CellArea.y )

        };

        return tilePositionOnGrid;

    }

    /// <summary>
    ///  Converts tilespace coordinates to a worldspace position.
    /// </summary>
    public Vector3 TileToWorldPosition( int xIndex, int yIndex )
    {
        return new Vector2( xIndex, yIndex ) + Origin;

    }

    /// <summary>
    /// Returns the tile object from the given world position.
    /// </summary>
    /// <remarks> * Only returns an object if it's in the grid map's boundaries. </remarks>>
    public T WorldToTile( Vector3 worldPosition )
    {
        Vector2Int positionToGrid = WorldToTilePosition( worldPosition );

        if( !IsIndexInMap( positionToGrid.x, positionToGrid.y ) )
        {
            return default;

        }

        return this[positionToGrid.x, positionToGrid.y];

    }

    /// <summary>
    /// Finds all tiles that are within the given radius around the xIndex and yIndex.
    /// </summary>
    public IEnumerable<T> GetSurroundingTiles( int xIndex, int yIndex, int radius, RadiusShape radiusShape, bool omitStartIndex = true )
    {
        for( int x = -radius; x <= radius; x++ )
        {
            for( int y = -radius; y <= radius; y++ )
            {
                // Cull the starting index from the search function.
                if( x == 0 && y == 0 && omitStartIndex )
                {
                    continue;
                }

                int xNeighborIndex = xIndex + x;
                int yNeighborIndex = yIndex + y;

                // This checks the distance of all tiles in the loop and culls any that go out of bounds of the radius.
                // https://chris3606.github.io/GoRogue/articles/grid_components/measuring-distance.html
                // Helpful little link ^
                switch( radiusShape )
                {
                    case RadiusShape.DIAMOND:
                        if( CalculateManhattanDistance( xIndex, yIndex, xNeighborIndex, yNeighborIndex ) > radius )
                            continue;
                        break;

                    case RadiusShape.CIRCLE:
                        if( CalculateEuclideanDistance( xIndex, yIndex, xNeighborIndex, yNeighborIndex ) > radius + 0.41f )
                            continue;
                        break;

                    case RadiusShape.SQUARE:
                        // The default behaviour already calculates all neighbors in a square, so you won't see the square shape
                        // have any extra logic or implementation.
                        break;

                }

                // If the tile wasn't out of bounds of the radius, and is in the map - add it to the list.
                if( IsIndexInMap( xNeighborIndex, yNeighborIndex ) )
                {
                    yield return this[xNeighborIndex, yNeighborIndex];
                }
            }
        }
    }

    /// <summary>
    /// Finds all tiles that are within the given radius around the world position.
    /// </summary>
    public IEnumerable<T> GetSurroundingTiles( Vector3 worldPosition, int radius, RadiusShape radiusShape )
    {
        Vector2Int positionToGrid = WorldToTilePosition( worldPosition );

        return GetSurroundingTiles( positionToGrid.x, positionToGrid.y, radius, radiusShape );

    }

    /// <summary>
    /// A faster method to get the imediate surrounding tiles around the x and y index.
    /// </summary>
    public IEnumerable<T> GetSurroundingTiles( int xIndex, int yIndex, DirectionType directionType )
    {
        foreach( Direction2D direction in directionType )
        {
            int xNeighborIndex = xIndex + direction.X;
            int yNeighborIndex = yIndex + direction.Y;

            if( IsIndexInMap( xIndex, yIndex ) )
            {
                yield return this[xNeighborIndex, yNeighborIndex];

            }
        }
    }

    /// <summary>
    /// A faster method to get the imediate surrounding tiles around the world position.
    /// </summary>
    public IEnumerable<T> GetSurroundingTiles( Vector3 worldPosition, DirectionType directionType )
    {
        Vector2Int positionToGrid = WorldToTilePosition( worldPosition );

        return GetSurroundingTiles( positionToGrid.x, positionToGrid.y, directionType );

    }

    /// <summary>
    /// Calculates the distance between two points using right angles.
    /// </summary>
    public int CalculateManhattanDistance( int xStartingIndex, int yStartingIndex, int xTargetIndex, int yTargetIndex )
    {
        int xNeighbourDistance = Mathf.Abs( xStartingIndex - xTargetIndex );
        int yNeighbourDistance = Mathf.Abs( yStartingIndex - yTargetIndex );

        return xNeighbourDistance + yNeighbourDistance;

    }

    /// <summary>
    /// Calculates the distance between two points using the logical sqrt of a circle.
    /// </summary>
    public float CalculateEuclideanDistance( int xStartingIndex, int yStartingIndex, int xTargetIndex, int yTargetIndex )
    {
        float xNeighborDistance = Mathf.Pow( xTargetIndex - xStartingIndex, 2.0f );
        float yNeighborDistance = Mathf.Pow( yTargetIndex - yStartingIndex, 2.0f );

        return Mathf.Sqrt( xNeighborDistance + yNeighborDistance );

    }
}
