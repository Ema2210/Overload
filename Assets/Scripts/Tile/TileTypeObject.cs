

using UnityEngine.Tilemaps;

public class TileTypeObject
{
    public TileType TileType { get; private set; }
    public bool HasMinerals { get; private set; } = true;
    public bool IsDiggable { get; private set; } = true;

    public Tile tile { get; private set; }

    public TileTypeObject(TileType tileType, Tile tile)
    {
        TileType = tileType;
        this.tile = tile;
        switch (tileType)
        {
            case TileType.None:
                HasMinerals = false;
                break;

            case TileType.Hole:
                HasMinerals = false;

                break;

            case TileType.Rock:
                HasMinerals = false;
                IsDiggable = false;
                break;


            default:
                HasMinerals = true;
                break;
        }
    }
}
