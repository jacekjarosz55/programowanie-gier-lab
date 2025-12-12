using UnityEngine;

public interface IInteractable
{
    public void OnFocusEnter(Player player);
    public void OnFocusLeave(Player player);
    public void OnActivate(Player player);
    public void OnDeactivate(Player player);
    public bool ShouldPickup { get; }
    public GameObject GameObject { get; }
}