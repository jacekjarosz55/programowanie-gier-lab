using UnityEngine;

public class Item
{
    public string Name { get; set; }
    public float Value {get; set;}
    public virtual ItemUseResult OnUse(Player user)
    {
        Debug.Log($"Item '{Name}' used!");
        return ItemUseResult.Normal;
    }
}

public enum ItemUseResult
{
    Normal,
    Consume
}