public struct GridObject
{
    public GridObject(int x, int y, TileType tileType)
    {
        X = x;
        Y = y;
        TileType = tileType;
    }

    public int X { get; }
    public int Y { get; }

    public TileType TileType { get; private set; }

    public void SetAsHole()
    {
        TileType = TileType.Hole;
    }

}

