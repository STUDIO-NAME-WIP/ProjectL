using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private Interactor interactor;
    [SerializeField] private PickUpper pickUpper;

    [SerializeField] private CollisionHandler collisionHandler;
    [SerializeField] private TriggerHandler triggerHandler;
    
    private InputSystemActions input;
    private PlayerModel model;

    private void Awake()
    {
        model = new PlayerModel(movement, interactor);
        input = new InputSystemActions();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;
        input.Player.Interact.performed += OnInteract;
        triggerHandler.OnTriggerEnterHandler += HandleTriggerEnter;
        triggerHandler.OnTriggerExitHandler += HandleTriggerExit;
    }
    
    private void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>(); 
        Vector3 movementDirection = new Vector3(direction.x, 0, direction.y);
        movement.SetMovement(movementDirection);
        movement.SetRotation(movementDirection);
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        interactor.Interact();
        pickUpper.Grab();
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;
        input.Player.Interact.performed -= OnInteract;
        triggerHandler.OnTriggerEnterHandler -= HandleTriggerEnter;
        triggerHandler.OnTriggerExitHandler -= HandleTriggerExit;
    }

    private void HandleTriggerEnter(Collider other)
    {
        interactor.TryAddInteractable(other);
        pickUpper.TryAddGrababble(other);
    }

    private void HandleTriggerExit(Collider other)
    {
        interactor.TryRemoveInteractable(other);
        pickUpper.TryRemoveInteractable(other);
    }
}
