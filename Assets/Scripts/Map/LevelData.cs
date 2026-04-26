using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Game/Level Data", order = 1)]
public class LevelData : ScriptableObject
{
    public int width;
    public int height;
    public float tileSize = 1f;
    public Vector3 origin = Vector3.zero;
    //TODO Add objects to the level
}
