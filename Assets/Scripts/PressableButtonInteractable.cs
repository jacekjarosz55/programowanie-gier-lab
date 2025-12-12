using UnityEngine;

public class PressableButtonInteractable : MonoBehaviour, IInteractable
{
    public Material onMaterial;
    private Material offMaterial;

    private Renderer rendererComponent;

    private bool activated = false;

    public bool ShouldPickup => false;

    public GameObject GameObject => gameObject;

    public void OnActivate(Player player)
    {
        Debug.Log("[Button] Activate!");
        activated = !activated;
        rendererComponent.material =  activated ? onMaterial : offMaterial;
        
    }

    public void OnDeactivate(Player player)
    {
        
    }

    public void OnFocusEnter(Player player)
    {
        Debug.Log("[Button] Focused!");
       
    }

    public void OnFocusLeave(Player player)
    {
        Debug.Log("[Button] De-Focused!");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rendererComponent = GetComponent<Renderer>();
        offMaterial = rendererComponent.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
