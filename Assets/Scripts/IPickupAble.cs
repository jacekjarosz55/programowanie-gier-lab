using UnityEngine;

public interface IPickupAble
{
    public Transform Transform { get; }
    public  Rigidbody Rigidbody { get; }
    public  Collider Collider { get; }

}
