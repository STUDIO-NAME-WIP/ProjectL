using UnityEngine;

public class PlayerModel
{
    private Movement movement;
    private Interactor interactor;

    public PlayerModel(Movement movement = null, Interactor interactor = null)
    {
        this.movement = movement;
        this.interactor = interactor;
    }
    
    
}
