using System;
using UnityEngine;


public abstract class SimpleInteractable : MonoBehaviour, IInteractable 
{
    public bool ShouldPickup => false;

    public GameObject GameObject => gameObject;

    public abstract void Activate(Player player);


    public void OnActivate(Player player)
    {
        // TODO: play sound here
        // make sound overridable
        Activate(player);
        Destroy(gameObject);
    }

    public void OnDeactivate(Player player)
    {
    }

    public void OnFocusEnter(Player player)
    {
    }

    public void OnFocusLeave(Player player)
    {
    }
}
