using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : LevelObject
{
    [SerializeField] private Interactor interactor;
    [SerializeField] private PickUpper pickUpper;
    [SerializeField] private CollisionHandler collisionHandler;
    [SerializeField] private TriggerHandler triggerHandler;
    private InputSystemActions input;
    private PlayerModel model;
    private PlayerView view;
    private Vector2 movementInput;

    private void Awake()
    {
        view = GetComponent<PlayerView>();
        input = new InputSystemActions();
    }

    public override void Initialize(LevelObjectData data, GridMap gridMap)
    {
        base.Initialize(data, gridMap);

        model = new PlayerModel(this, null);
        input.Player.Move.started += OnMove;
        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;
        input.Player.Interact.started += OnInteract;

        if (triggerHandler != null)
        {
            triggerHandler.OnTriggerEnterHandler += HandleTriggerEnter;
            triggerHandler.OnTriggerExitHandler += HandleTriggerExit;
        }

        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.started -= OnMove;
        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;
        input.Player.Interact.started -= OnInteract;

        if (triggerHandler != null)
        {
            triggerHandler.OnTriggerEnterHandler -= HandleTriggerEnter;
            triggerHandler.OnTriggerExitHandler -= HandleTriggerExit;
        }
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (model != null && movementInput.sqrMagnitude > 0.0001f)
            model.TryMove(movementInput);
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (pickUpper != null && pickUpper.HasPriority)
        {
            pickUpper.Act();
            return;
        }

        if (interactor != null && interactor.HasObjectsNearby())
        {
            interactor.Act();
            return;
        }

        if (pickUpper != null && pickUpper.HasObjectsNearby())
        {
            pickUpper.Act();
            return;
        }

        Debug.LogWarning("[PlayerController] No interactive or grabbable object on facing tile");
    }

    private void HandleTriggerEnter(Collider other)
    {
        if (interactor != null)
            interactor.TryAddObject(other);
        if (pickUpper != null)
            pickUpper.TryAddObject(other);
    }

    private void HandleTriggerExit(Collider other)
    {
        if (interactor != null)
            interactor.TryRemoveObject(other);
        if (pickUpper != null)
            pickUpper.TryRemoveObject(other);
    }
}
