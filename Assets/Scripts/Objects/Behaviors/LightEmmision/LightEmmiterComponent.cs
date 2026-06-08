using UnityEngine;

public class LightEmmiterComponent : MonoBehaviour, IObjectBehavior
{
    private ILightEmitterBehavior emitter;
    private int range;
    private Direction orientation;

    public void Configure(LevelObjectParameters data)
    {
        range = data.lightRange;
        switch (data.lightShape)
        {
            case LightShape.CONE:
                emitter = new ConeLightEmmiterBehavior(range);
                break;
            case LightShape.AREA:
                emitter = new SquareLightEmmiterBehavior(range);
                break;
            case LightShape.LINE:
                emitter = new LineLightEmmiterBehavior(range);
                break;
            case LightShape.NONE:
            default:
                emitter = new NoLightEmmiterBehavior();
                break;
        }
    }

    public void ApplyIllumination(Tile originTile, GridMap map)
    {
        foreach (var pos in emitter.GetIlluminatedTiles(originTile.GridPosition, orientation))
        {
            var tile = map.GetTile(pos);
            if (tile != null)
                tile.AddLightSource(this);
        }
    }

    public void RemoveIllumination(Tile originTile, GridMap map)
    {
        foreach (var pos in emitter.GetIlluminatedTiles(originTile.GridPosition, orientation))
        {
            var tile = map.GetTile(pos);
            if (tile != null)
                tile.RemoveLightSource(this);
        }
    }
}

public enum LightShape
{
    NONE,
    CONE,
    AREA,
    LINE
}
