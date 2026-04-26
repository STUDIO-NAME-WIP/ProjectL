using UnityEngine;

public class GridMap : MonoBehaviour
{
    private int width;
    private int height;
    private float tileSize;
    private Vector3 origin;
    private Tile[,] tiles;

    public void Initialize(LevelData levelData)
    {
        width = levelData.width;
        height = levelData.height;
        tileSize = levelData.tileSize;
        origin = levelData.origin;
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 worldPosition = origin + new Vector3 (x * tileSize, 0, y * tileSize);
                tiles[x, y] = new Tile(new Vector2Int(x, y), worldPosition);
            }
        }
    }

    public Tile GetTile(Vector2Int gridPosition)
    {
        if (!IsValidPosition(gridPosition)) return null;
        return tiles[gridPosition.x, gridPosition.y];
    }

    public bool IsValidPosition(Vector2Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < width &&
               gridPosition.y >= 0 && gridPosition.y < height;
    }

    public Vector2Int WorldToGrid(Vector3 worldPosition)
    {
        Vector3 relativePosition = worldPosition - origin;

        int x = Mathf.RoundToInt(relativePosition.x / tileSize);
        int y = Mathf.RoundToInt(relativePosition.z / tileSize);
        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(Vector2Int gridPosition)
        {
            return origin + new Vector3(gridPosition.x * tileSize, 0, gridPosition.y * tileSize);
    }
}
