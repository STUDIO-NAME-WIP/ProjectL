using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerHandler : MonoBehaviour
{
    public Action<Collider> OnTriggerEnterHandler;
    public Action<Collider> OnTriggerStayHandler;
    public Action<Collider> OnTriggerExitHandler;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterHandler?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayHandler?.Invoke(other);
    }
    
    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitHandler?.Invoke(other);
    }
}
