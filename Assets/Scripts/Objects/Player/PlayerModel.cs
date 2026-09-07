using UnityEngine;

public class PlayerModel
{
    private ObjectHandler<IInteractable> interactor;
    private PlayerController player;
    private MovableComponent movable;

    public PlayerModel(PlayerController playerController, ObjectHandler<IInteractable> interactor = null)
    {
        player = playerController;
        movable = player.GetBehavior<MovableComponent>();
        this.interactor = interactor;
    }

    public void TryMove(Vector2 direction)
    {
        if (movable != null && movable.TryMove(player, direction))
            player.Rotate(direction.ToDirection());
    }
}
