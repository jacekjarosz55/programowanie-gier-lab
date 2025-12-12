using UnityEngine;
using UnityEngine.InputSystem;

public class FreeCameraMotion : MonoBehaviour
{

    public InputActionProperty freeCameraMoveAction;
    public float speed = 10.0f;
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (cam.enabled && !Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        if (!cam.enabled && Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Vector2 moveDirection = freeCameraMoveAction.action.ReadValue<Vector2>();
        transform.position = transform.position + (new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * speed);
    }
}
