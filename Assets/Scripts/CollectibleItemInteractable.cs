using UnityEngine;

public class CollectibleItemInteractable : SimpleInteractable
{
    public string Name; 
    public int Value;

    public override void Activate(Player player)
    {
        player.inventory.Add(new Item{Name=Name, Value=Value});
    }
}
