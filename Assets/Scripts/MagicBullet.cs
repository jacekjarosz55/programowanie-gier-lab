using System.Collections;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public GameObject explosionParticles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += 10.0f * Time.deltaTime * transform.forward;
        StartCoroutine(KillCooldown());
    }

    private IEnumerator KillCooldown()
    {
        yield return new WaitForSeconds(4);
        Explode();
        yield return null;
    }
    private void Explode()
    {
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
