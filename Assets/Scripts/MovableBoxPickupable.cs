using UnityEngine;


public class MovableBoxPickupable : MonoBehaviour, IPickupAble
{
    private Rigidbody rb;
    private Collider col;
    public GameObject GameObject => gameObject;

    public Rigidbody Rigidbody => rb;
    public Collider Collider => col;
    public Transform Transform => transform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
