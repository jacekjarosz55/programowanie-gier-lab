using UnityEngine;

public class Target : MonoBehaviour
{

    public Material hitMaterial;

    private MeshRenderer meshRenderer;

    //private bool hit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //hit = true;
        meshRenderer.material = hitMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
