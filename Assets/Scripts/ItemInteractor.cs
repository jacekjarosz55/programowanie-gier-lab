using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemInteractor : MonoBehaviour
{
    private Transform itemAttach;
    private Transform hitAnchor;

    private Canvas messageCanvas;

    private IInteractable currentInteracted = null;

    private IPickupAble currentPickUpAble = null;
    private IPickupAble pickedUp = null;


    public IInteractable CurrentInteracted => currentInteracted;
    public IPickupAble PickedUp => pickedUp;
    public Player player;

    public float maxDistance = 1f;
    private int layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("Interaction");
        itemAttach = GameObject.Find("ItemAttach").transform;
        hitAnchor = GameObject.Find("HitAnchor").transform;
        messageCanvas = GameObject.Find("Message").GetComponent<Canvas>();
    }

    private void PickUp()
    {
        pickedUp = currentPickUpAble;
        pickedUp.Collider.enabled = false;
        pickedUp.Rigidbody.isKinematic = true;
        pickedUp.Transform.SetParent(itemAttach);
    }
    private void PutDown()
    {
        pickedUp.Collider.enabled = true;
        pickedUp.Rigidbody.isKinematic = false;
        pickedUp.Transform.SetParent(null);
        pickedUp = null;
    }


    public void StartPickup()
    {
        if (currentPickUpAble == null) return;
        PickUp();

    }

    public void StopPickup()
    {
        if (pickedUp != null)
        {
            PutDown();
            return;
        }

    }

    public void Activate()
    {
        if (currentInteracted == null) return;
        currentInteracted.OnActivate(player);
    } 
    public void Deactivate()
    {
        if (currentInteracted == null) return;
        currentInteracted.OnDeactivate(player);
    } 

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        bool hasHit = Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerMask);
        messageCanvas.enabled = hasHit; 
        if (hasHit)
        {
            IInteractable interacted;
            IPickupAble pickupAble;

            hitAnchor.position = hit.transform.position;

            if (hit.transform.TryGetComponent(out interacted))
            {

                if (currentInteracted != interacted)
                {
                    interacted.OnFocusEnter(player);
                }
                currentInteracted = interacted;                
            }

            if (hit.transform.TryGetComponent(out pickupAble))
            {
                currentPickUpAble = pickupAble;                
            }


            // Show Interaction Message    

        }
        else
        {
            if (currentInteracted != null)
            {
                currentInteracted.OnFocusLeave(player);
            }
            currentInteracted = null;
            currentPickUpAble = null;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;    
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * maxDistance);
    }
}
