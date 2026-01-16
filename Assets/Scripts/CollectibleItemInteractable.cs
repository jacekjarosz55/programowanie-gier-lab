using UnityEngine;

public class CollectibleItemInteractable : SimpleInteractable
{
    public string Name; 
    public int Value;

    public override void Activate(Player player)
    {
        player.AddItem(new Item{Name=Name, Value=Value});
    }
}
