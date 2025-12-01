using UnityEngine;

public interface IInteractable
{
    public void OnFocusEnter();
    public void OnFocusLeave();
    public void OnActivate();
    public void OnDeactivate();
    public bool ShouldPickup { get; }
    public GameObject GameObject { get; }
}