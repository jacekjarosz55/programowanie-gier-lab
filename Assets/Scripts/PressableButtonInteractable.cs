using UnityEngine;

public class PressableButtonInteractable : MonoBehaviour, IInteractable
{
    public Material onMaterial;
    private Material offMaterial;

    private Renderer rendererComponent;

    private bool activated = false;

    public bool ShouldPickup => false;

    public GameObject GameObject => gameObject;

    public void OnActivate()
    {
        Debug.Log("[Button] Activate!");
        activated = !activated;
        rendererComponent.material =  activated ? onMaterial : offMaterial;
        
    }

    public void OnDeactivate()
    {
        
    }

    public void OnFocusEnter()
    {
        Debug.Log("[Button] Focused!");
       
    }

    public void OnFocusLeave()
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
