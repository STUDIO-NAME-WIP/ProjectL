using UnityEngine;

public class Reflector : LevelObject
{
    public void Setup()
    {
        var interactable = GetBehavior<InteractableComponent>();
        interactable.SetupAction(Reflect);
    }

    public void Reflect(InteractionData data)
    {
        LightEmmiterComponent lightEmmiter = GetBehavior<LightEmmiterComponent>();

        lightEmmiter.RemoveIllumination(currentTile, map, Orientation);
        Direction newDirection = Orientation.RotateClockwise();
        Rotate(newDirection);
        lightEmmiter.ApplyIllumination(currentTile, map, Orientation);
    }

#if UNITY_EDITOR
    [ExecuteInEditMode]
    public void TestReflect()
    {
        Reflect(new InteractionData());
    }
#endif
}