public struct MovementData
{
    public LevelObject obj;
    public Tile originTile;
    public Tile targetTile;
    public TileLayer layer;

    public MovementData(LevelObject obj, Tile originTile, Tile targetTile, TileLayer layer)
    {
        this.obj = obj;
        this.originTile = originTile;
        this.targetTile = targetTile;
        this.layer = layer;

    }
}