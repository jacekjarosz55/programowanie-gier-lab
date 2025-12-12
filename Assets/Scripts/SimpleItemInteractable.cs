using System;
using UnityEngine;
using UnityEngine.Audio;


public abstract class SimpleInteractable : MonoBehaviour, IInteractable 
{
    public bool ShouldPickup => false;

    public GameObject spawnOnPickup;
    
    public GameObject GameObject => gameObject;


    public abstract void Activate(Player player);


    public void OnActivate(Player player)
    {
        if (spawnOnPickup != null)
        {
            Instantiate(spawnOnPickup, transform.position, transform.rotation);
        }
        Activate(player);
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
}
