using UnityEngine;

public class HealthItemInteractable : SimpleInteractable
{
    public float healthDelta = +10;
    public override void Activate(Player player)
    {
        player.ChangeHealth(healthDelta);
    }
}
