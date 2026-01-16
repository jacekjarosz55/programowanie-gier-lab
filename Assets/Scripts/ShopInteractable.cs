using UnityEngine;

class ShopInteractable : MonoBehaviour, IInteractable
{
    public bool ShouldPickup => false;

    public GameObject GameObject => gameObject;

    public void OnActivate(Player player)
    {
        player.EnterShop();
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