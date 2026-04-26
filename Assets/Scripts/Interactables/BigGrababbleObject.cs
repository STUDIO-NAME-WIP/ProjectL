using UnityEngine;

public class BigGrababbleObject : MonoBehaviour, IGrababble
{
    public Transform playerController;
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Grab()
    {
        transform.position = playerController.position + new Vector3(0, 5, 0);
        transform.rotation = playerController.rotation;
        transform.SetParent(playerController);
    }

    public void Drop()
    {
        transform.SetParent(null);
        transform.position = playerController.position + new Vector3(0, 0, 2);
    }
}
