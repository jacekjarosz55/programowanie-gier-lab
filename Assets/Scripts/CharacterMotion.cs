using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMotion : MonoBehaviour
{
    private CharacterController controller;
    private Transform camPivot;

    public InputActionProperty lookAction;
    public InputActionProperty movementAction;
    public InputActionProperty jumpAction;
    public InputActionProperty crouchAction;

    public float sensitivity = 1.0f;


    public float jumpForce = 40.0f;
    private float jumpVelocity = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        jumpAction.action.performed += JumpAction_Performed;
        crouchAction.action.performed += CrouchAction_Performed;
        camPivot = GameObject.Find("Camera Pivot").transform;
    }

    private void CrouchAction_Performed(InputAction.CallbackContext context)
    {
        Debug.Log("crouch");
    }

    private void JumpAction_Performed(InputAction.CallbackContext obj)
    {
        Debug.Log("jump");
        if (controller.isGrounded)
        {
            jumpVelocity = jumpForce;
        }
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = movementAction.action.ReadValue<Vector2>();
        float gravityY = Physics.gravity.y;

        float zMotion = direction.y * 1.0f;
        float xMotion = direction.x * 1.0f;
        float yMotion = jumpVelocity + gravityY;

        controller.Move((transform.forward*zMotion + transform.right * xMotion + transform.up * yMotion) * Time.deltaTime);
        Vector2 look = lookAction.action.ReadValue<Vector2>();
        transform.Rotate(Vector3.up, look.x * sensitivity);

        jumpVelocity = Mathf.MoveTowards(jumpVelocity, 0, Time.deltaTime);


        
        Vector3 newEulerRotation = camPivot.eulerAngles + new Vector3(-look.y * sensitivity, 0f, 0f);
        newEulerRotation.x = ClampAngle(newEulerRotation.x, -90.0f, 90.0f);
        camPivot.eulerAngles = newEulerRotation;
    }
}
