using UnityEngine;
using UnityEngine.InputSystem;

public class FreeCameraMotion : MonoBehaviour
{

    public InputActionProperty freeCameraMoveAction;
    public float speed = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = freeCameraMoveAction.action.ReadValue<Vector2>();
        transform.position = transform.position + (new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * speed);
    }
}
