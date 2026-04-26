using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Vector2Int GridPosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public bool IsLit { get; set; }

    private Dictionary<TileLayer, ITileContent> contents;

    public Tile(Vector2Int gridPosition, Vector3 worldPosition)
    {
        GridPosition = gridPosition;
        WorldPosition = worldPosition;
        contents = new Dictionary<TileLayer, ITileContent>();
    }

    public bool TryPlaceContent(TileLayer layer, ITileContent content)
    {
        if (contents.ContainsKey(layer)) return false;
        contents[layer] = content;
        //content.OnPlaced(this); TODO: Check if this is needed
        return true;
    }

    public ITileContent GetContent(TileLayer layer)
    {
        contents.TryGetValue(layer, out ITileContent content);
        return content;
    }
}