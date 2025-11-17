using System.Collections;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public GameObject explosionParticles;

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 20.0f, ForceMode.VelocityChange);
        StartCoroutine(KillCooldown());
    }

    private IEnumerator HitKillCooldown()
    {
        yield return new WaitForSeconds(5);
        Explode();
        yield return null;
    }

    private IEnumerator KillCooldown()
    {
        yield return new WaitForSeconds(10);
        Explode();
        yield return null;
    }


    private void Explode()
    {
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(HitKillCooldown());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
