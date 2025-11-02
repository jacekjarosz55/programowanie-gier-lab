using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



public class CharacterMotion : MonoBehaviour
{

    private bool isJumping = false;
    private bool isCrouching = false;

    private CharacterController controller;
    private Transform camPivot;


    public Animator animator;

    public InputActionProperty lookAction;
    public InputActionProperty movementAction;
    public InputActionProperty jumpAction;
    public InputActionProperty crouchAction;

    public float sensitivity = 1.0f;
    public float walkSpeed = 1.0f;
    public float crouchSpeed = 0.5f;


    public float jumpForce = 20.0f;
    public float jumpAttentuation = 10.0f;
    private float jumpVelocity = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        jumpAction.action.performed += JumpAction_Performed;
        crouchAction.action.started += CrouchAction_Started;
        crouchAction.action.canceled += CrouchAction_Cancelled;
        camPivot = GameObject.Find("Camera Pivot").transform;
    }


    private void CrouchAction_Started(InputAction.CallbackContext context)
    {
        if(!isJumping && controller.isGrounded)
        {
            Debug.Log("crouch start!");
            isCrouching = true;
            animator.SetBool("crouching", true);
        }
    }

    private void CrouchAction_Cancelled(InputAction.CallbackContext context)
    {
        if (isCrouching)
        {
            Debug.Log("crouch end!");
            isCrouching = false;
            animator.SetBool("crouching", false);
        }
    }


    private void JumpAction_Performed(InputAction.CallbackContext obj)
    {
        if (controller.isGrounded && !isJumping && !isCrouching)
        {
            Debug.Log("jumping!");
            isJumping = true;
            animator.SetBool("jumping", true);
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
        if (isJumping && controller.isGrounded && jumpVelocity == 0)
        {
            isJumping = false;
            animator.SetBool("jumping", false);
        }

        Vector2 direction = movementAction.action.ReadValue<Vector2>();
        float gravityY = Physics.gravity.y;

        float moveSpeed = isCrouching ? crouchSpeed : walkSpeed;

        float zMotion = direction.y * moveSpeed;
        float xMotion = direction.x * moveSpeed;
        float yMotion = jumpVelocity + gravityY;

        animator.SetBool("walking", (Math.Abs(xMotion) > 0 || Math.Abs(zMotion) > 0));

        Debug.Log($"isJumping = {isJumping}");
        Debug.Log($"jumpVelocity = {jumpVelocity}");
        Debug.Log($"gravityY = {gravityY}");
        Debug.Log($"yMotion = {yMotion}");

        controller.Move((transform.forward * zMotion + transform.right * xMotion + transform.up * yMotion) * Time.deltaTime);

        jumpVelocity = Mathf.MoveTowards(jumpVelocity, 0, jumpAttentuation * Time.deltaTime);


        Vector2 look = lookAction.action.ReadValue<Vector2>();
        transform.Rotate(Vector3.up, look.x * sensitivity);

        Vector3 newEulerRotation = camPivot.eulerAngles + new Vector3(-look.y * sensitivity, 0f, 0f);
        newEulerRotation.x = ClampAngle(newEulerRotation.x, -90.0f, 90.0f);
        camPivot.eulerAngles = newEulerRotation;
    }
}
