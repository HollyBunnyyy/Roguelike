using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public struct Direction2D
{
    private readonly string _name;
    private readonly Vector2Int _value;

    public readonly int X => _value.x;
    public readonly int Y => _value.y;

    private Direction2D( string directionName, Vector2Int directionValue )
    {
        this._name  = directionName;
        this._value = directionValue;

    }

    public override string ToString()
    {
        return _name;

    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();

    }

    public bool Equals( Direction2D directionToCompare )
    {
        // faster to compare direction values instead of direction name strings.
        return directionToCompare._value == this._value;

    }

    public override bool Equals( object obj )
    {
        if( obj is null )
        {
            return false;

        }

        return obj is Direction2D direction && Equals( direction );

    }

    public readonly static Direction2D Up           = new Direction2D( "Up",            new Vector2Int(  0,  1 ) );
    public readonly static Direction2D TopRight     = new Direction2D( "Top Right",     new Vector2Int(  1,  1 ) );
    public readonly static Direction2D Right        = new Direction2D( "Right",         new Vector2Int(  1,  0 ) );
    public readonly static Direction2D BottomRight  = new Direction2D( "BottomRight",   new Vector2Int(  1, -1 ) );
    public readonly static Direction2D Down         = new Direction2D( "Down",          new Vector2Int(  0, -1 ) );
    public readonly static Direction2D BottomLeft   = new Direction2D( "Bottom Left",   new Vector2Int( -1, -1 ) );
    public readonly static Direction2D Left         = new Direction2D( "Left",          new Vector2Int( -1,  0 ) );
    public readonly static Direction2D TopLeft      = new Direction2D( "TopLeft",       new Vector2Int( -1,  1 ) );

    internal readonly static Direction2D[] DirectionsCardinal = new Direction2D[4]
    {
        Up, Right, Down, Left

    };

    internal readonly static Direction2D[] DirectionsDiagonal = new Direction2D[4]
    {
        TopRight, BottomRight, BottomLeft, TopLeft

    };

    internal readonly static Direction2D[] DirectionsOrdinal = new Direction2D[8]
    {
        Up, TopRight, Right, BottomRight, Down, BottomLeft, Left, TopLeft

    };

    // If you add 4 to the index of any direction on the DirectionsOrdinal array, you get the opposite direction.
    // The modulo operator makes it loop through - back to the beginning index. 
    // The array is a fixed static size, so no null values or overflow is possible with the modulo.
    public static Direction2D Opposite( Direction2D direction )
    {
        return DirectionsOrdinal[( Array.IndexOf( DirectionsOrdinal, direction ) + 4 ) % DirectionsOrdinal.Length];

    }

    public static bool operator ==( Direction2D a, Direction2D b )
    {
        return a.Equals( b );

    }

    public static bool operator !=( Direction2D a, Direction2D b )
    {
        return !( a == b );

    }

    public static implicit operator Vector2Int( Direction2D direction ) => direction._value;

}

public struct DirectionTypeEnumerator : IEnumerator
{
    private int _index;

    private Direction2D[] _directions;

    public DirectionTypeEnumerator( Direction2D[] directions )
    {
        this._index = -1;
        this._directions = directions;

    }

    public bool MoveNext()
    {
        _index++;

        return _index < _directions.Length;

    }

    public void Reset()
    {
        _index = -1;

    }

    object IEnumerator.Current => Current;

    public Direction2D Current
    {
        get {
            try 
            { 
                return _directions[_index]; 
            } 

            catch( IndexOutOfRangeException ) 
            {
                throw new InvalidOperationException();

            }
        }
    }
}

[System.Serializable]
public struct DirectionType : IEnumerable
{
    private readonly Direction2D[] _directions;

    private DirectionType( Direction2D[] directions )
    {
        _directions = directions;

    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();

    }

    public DirectionTypeEnumerator GetEnumerator()
    {
        return new DirectionTypeEnumerator( _directions );

    }

    /// <summary> 
    /// Up, Right, Down, Left 
    /// </summary>
    public readonly static DirectionType Cardinal   = new DirectionType( Direction2D.DirectionsCardinal );

    /// <summary>
    /// Top Right, Bottom Right, Bottom Left, Top Left
    /// </summary>
    public readonly static DirectionType Diagonals  = new DirectionType( Direction2D.DirectionsDiagonal );

    /// <summary> 
    /// Up, TopRight, Right, BottomRight, Down, BottomLeft, Left, TopLeft
    /// </summary>
    public readonly static DirectionType Ordinal    = new DirectionType( Direction2D.DirectionsOrdinal );

}
