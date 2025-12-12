using UnityEngine;

public class AmmoItemInteractable : SimpleInteractable
{
    public override void Activate(Player player)
    {
        player.Ammo += 10;
    }
}
