using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovableComponent))]
[RequireComponent(typeof(InteractableComponent))]
[RequireComponent(typeof(LightEmmiterComponent))]
[RequireComponent(typeof(LightBlockerComponent))]
[RequireComponent(typeof(ColliderComponent))]
[RequireComponent(typeof(StateComponent))]
[RequireComponent(typeof(ActivableComponent))]
public class LevelObject : MonoBehaviour, ITileContent
{
    public string ObjectId { get; protected set; }
    public Vector2Int GridPosition { get; set; }
    public TileLayer Layer { get; protected set; }
    public Direction Orientation { get; protected set; }

    protected Tile currentTile;
    protected LevelObjectData data;
    protected Dictionary<Type, IObjectBehavior> behaviors = new Dictionary<Type, IObjectBehavior>();

    public virtual void Initialize(LevelObjectData objectData)
    {
        data = objectData;
        ObjectId = objectData.objectId;
        GridPosition = objectData.gridPosition;
        Layer = objectData.layer;
        Orientation = objectData.orientation;

        transform.rotation = Orientation.ToRotation();

        InitializeBehaviors();
    }

    protected virtual void InitializeBehaviors()
    { 
        behaviors.Add(typeof(MovableComponent), GetComponent<MovableComponent>());
        behaviors.Add(typeof(InteractableComponent), GetComponent<InteractableComponent>());
        behaviors.Add(typeof(LightEmmiterComponent), GetComponent<LightEmmiterComponent>());
        behaviors.Add(typeof(LightBlockerComponent), GetComponent<LightBlockerComponent>());
        behaviors.Add(typeof(ColliderComponent), GetComponent<ColliderComponent>());
        behaviors.Add(typeof(StateComponent), GetComponent<StateComponent>());
        behaviors.Add(typeof(ActivableComponent), GetComponent<ActivableComponent>());

        foreach (var behavior in behaviors.Values)
        {
            behavior.Configure(data.parameters);
        }
    }

    public T GetBehavior<T>() where T : IObjectBehavior
    {
        if (behaviors.TryGetValue(typeof(T), out var behavior))
        {
            return (T)behavior;
        }
        return default;
    }

    public virtual void Rotate(Direction newDirection)
    {
        Orientation = newDirection;
        transform.rotation = Orientation.ToRotation();
    }
}

public enum Direction
{
    NORTH, EAST, SOUTH, WEST
}

public static class DirectionExtensions
{
    public static Vector2Int ToVector2Int(this Direction dir)
    {
        return dir switch
        {
            Direction.NORTH => new Vector2Int(0, 1),
            Direction.EAST => new Vector2Int(1, 0),
            Direction.SOUTH => new Vector2Int(0, -1),
            Direction.WEST => new Vector2Int(-1, 0),
            _ => Vector2Int.zero,
        };
    }

    public static Quaternion ToRotation(this Direction dir)
    {
        return dir switch
        {
            Direction.NORTH => Quaternion.Euler(0, 0, 0),
            Direction.EAST => Quaternion.Euler(0, 90, 0),
            Direction.SOUTH => Quaternion.Euler(0, 180, 0),
            Direction.WEST => Quaternion.Euler(0, 270, 0),
            _ => Quaternion.identity,
        };
    }
}