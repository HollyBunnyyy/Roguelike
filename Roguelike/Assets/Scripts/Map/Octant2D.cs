public struct Octant2D 
{
    public readonly int XX;
    public readonly int XY;
    public readonly int YX;
    public readonly int YY;

    private Octant2D( int xx, int xy, int yx, int yy )
    {
        this.XX = xx;
        this.XY = xy;
        this.YX = yx;
        this.YY = yy;
    }

    public readonly static Octant2D[] OctantRegions = new Octant2D[8]
    {
        new Octant2D(  1,  0,  0,  1 ), //  E - NE
        new Octant2D(  0,  1,  1,  0 ), // NE - N
        new Octant2D(  0, -1,  1,  0 ), //  N - NW
        new Octant2D( -1,  0,  0,  1 ), // NW - W
        new Octant2D( -1,  0,  0, -1 ), //  W - SW
        new Octant2D(  0, -1, -1,  0 ), // SW - S
        new Octant2D(  0,  1, -1,  0 ), //  S - SE
        new Octant2D(  1,  0,  0, -1 )  // SE - E
    };
}
