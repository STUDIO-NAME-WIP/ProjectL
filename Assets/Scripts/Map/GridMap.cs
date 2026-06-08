using System.Linq;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    private Tile[,] tiles;
    private int width;
    private int height;
    private float tileSize;

    private Vector3 origin;

    [SerializeField]
    private LevelData levelData;

    private void Awake()
    {
        Initialize(levelData);
    }

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
                Vector3 worldPosition = origin + new Vector3(x * tileSize, 0, y * tileSize);
                Vector2Int gridPos = new Vector2Int(x, y);

                tiles[x, y] = new Tile(gridPos, worldPosition);
            }
        }

        var sortedObjects = levelData.levelObjects.OrderBy(obj => obj.layer).ToList();

        foreach (var objData in sortedObjects)
        {
            var instance = Instantiate(objData.prefab, GridToWorld(objData.gridPosition), objData.orientation.ToRotation());
            instance.name = $"{objData.objectId}";
            var levelObj = instance.GetComponent<LevelObject>();
            levelObj.Initialize(objData);

            var tile = GetTile(objData.gridPosition);
            tile?.TryPlaceObject(objData.layer, levelObj);
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
