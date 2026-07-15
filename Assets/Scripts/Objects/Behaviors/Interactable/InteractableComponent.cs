using System;
using UnityEngine;

public class InteractableComponent : MonoBehaviour, IObjectBehavior
{
    public IInteractableBehavior interactableBehavior;

    public void Configure(LevelObjectParameters data)
    {
        switch(data.interactionType)
        {
            case InteractionType.PICKUP:
                interactableBehavior = new PickupInteractionBehavior();
                break;
            case InteractionType.INTERACT:
                interactableBehavior = new InteractInteractionBehavior();
                break;
            case InteractionType.NONE:
            default:
                interactableBehavior = new NoInteractionBehavior();
                break;
        }
    }

    public void Interact(InteractionData interactionData)
    {
        interactableBehavior.PerformInteraction(interactionData);
    }

    public void SetupAction(Action<InteractionData> action)
    {
        if (interactableBehavior is InteractInteractionBehavior actionBehavior)
        {
            (interactableBehavior as InteractInteractionBehavior).OnInteract += action;
        }
    }
}

public enum InteractionType { NONE, PICKUP, INTERACT }