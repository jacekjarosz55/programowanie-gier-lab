using UnityEngine;

public class CollectibleItemInteractable : MonoBehaviour, IInteractable
{
    public string Name; 
    public int Value;

    public bool ShouldPickup => false;

    public GameObject GameObject => gameObject;

    public void OnActivate(Player player)
    {
        player.inventory.Add(new Item{Name=Name, Value=Value});
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
