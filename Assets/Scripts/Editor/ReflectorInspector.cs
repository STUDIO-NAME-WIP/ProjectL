using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Reflector))]
public class ReflectorInspector : Editor
{
    private Reflector reflector;

    public void OnEnable()
    {
        reflector = (Reflector)target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Interact"))
        {
            reflector.TestReflect();
        }
    }
}
