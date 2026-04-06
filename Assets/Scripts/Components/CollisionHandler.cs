using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public Action<Collision> OnCollisionEnterHandler;
    public Action<Collision> OnCollisionStayHandler;
    public Action<Collision> OnCollisionExitHandler;

    private void OnCollisionEnter(Collision other)
    {
        OnCollisionEnterHandler?.Invoke(other);
    }

    private void OnCollisionStay(Collision other)
    {
        OnCollisionStayHandler?.Invoke(other);
    }
    
    private void OnCollisionExit(Collision other)
    {
        OnCollisionExitHandler?.Invoke(other);
    }
}