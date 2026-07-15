using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : LevelObject
{
    [SerializeField] private Movement movement;
    [SerializeField] private Interactor interactor;
    [SerializeField] private PickUpper pickUpper;
    [SerializeField] private CollisionHandler collisionHandler;
    [SerializeField] private TriggerHandler triggerHandler;
    private InputSystemActions input;
    private PlayerModel model;
    private MovableComponent movable;

    private void Awake()
    {
        model = new PlayerModel(movement, null);
        input = new InputSystemActions();
    }

    public override void Initialize(LevelObjectData data, GridMap gridMap)
    {
        base.Initialize(data, gridMap);
        input.Enable();
        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;
        input.Player.Interact.started += OnInteract;

        if (triggerHandler != null)
        {
            triggerHandler.OnTriggerEnterHandler += HandleTriggerEnter;
            triggerHandler.OnTriggerExitHandler += HandleTriggerExit;
        }
        movable = GetBehavior<MovableComponent>();
    }

    private void OnDisable()
    {
        input.Disable();
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
        Vector2 direction = ctx.ReadValue<Vector2>();
        if (direction == Vector2.zero) return;

        Tile originTile = movable.GetCurrentTile();
        Vector2Int targetGridPos = originTile.GridPosition + Vector2Int.RoundToInt(direction);
        Tile targetTile = map.GetTile(targetGridPos);

        MovementData data = new MovementData(this, originTile, targetTile, TileLayer.OBJECT);

        if (movable.CanMove(data))
        {
            movable.Move(data);

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                Orientation = direction.x > 0 ? Direction.EAST : Direction.WEST;
            else
                Orientation = direction.y > 0 ? Direction.NORTH : Direction.SOUTH;
        }
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
