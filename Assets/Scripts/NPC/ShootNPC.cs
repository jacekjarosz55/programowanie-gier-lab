using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class ShootNPC : DialogNPC
{
    public GameObject bullet;
    public Transform bulletSpawnTransform;
    public Transform target;

    public ShootNPC() : base()
    {
        dialog = new TextDialog("DONT INTERRUPT ME!!!", new
            List<DialogOption>
        {
            new("Ok...", null)
        });

    }

    private bool canShoot = true;
    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(1.0f);
        canShoot = true; 
    }

    void Update()
    {
        transform.LookAt(target, Vector3.up);

        if (canShoot) {
            Instantiate(bullet, bulletSpawnTransform.position, transform.rotation);
            StartCoroutine(ShootCooldown());
        }
    }
}

