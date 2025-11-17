using UnityEngine;

public class DestructibleCrate : MonoBehaviour
{
    public GameObject debrisParticles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Instantiate(debrisParticles, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
