using System;
using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CharacterState
{
    Idle,
    Jumping,
    Crouching,
    Shooting
}

public class CharacterMotion : MonoBehaviour
{


    private CharacterState state = CharacterState.Idle;
    private bool isWalking = false;

    private CharacterController controller;
    private Transform camPivot;
    private Transform cameraTransform;

    private Transform luger;


    public Animator animator;

    public InputActionProperty lookAction;
    public InputActionProperty movementAction;
    public InputActionProperty jumpAction;
    public InputActionProperty crouchAction;
    public InputActionProperty shootAction;

    public InputActionProperty zoomAction;

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
        cameraTransform = GameObject.Find("Main Camera").transform;
        shootAction.action.performed += ShootAction_Performed;


        luger = GameObject.Find("Luger" ).transform;
        // xDDD
        luger.localScale = Vector3.zero;
    }



    private void ShootAction_Performed(InputAction.CallbackContext context)
    {
        if(state == CharacterState.Idle)
        {
            animator.SetTrigger("shoot");
            state = CharacterState.Shooting;
            StartCoroutine(ShootDelay());
        }
            
    }

    private void CrouchAction_Started(InputAction.CallbackContext context)
    {
        if(state != CharacterState.Jumping && controller.isGrounded)
        {
            Debug.Log("crouch start!");
            state = CharacterState.Crouching;
            animator.SetBool("crouching", true);
        }
    }

    private void CrouchAction_Cancelled(InputAction.CallbackContext context)
    {
        if (state == CharacterState.Crouching)
        {
            Debug.Log("crouch end!");
            state = CharacterState.Idle;
            animator.SetBool("crouching", false);
        }
    }


    private void JumpAction_Performed(InputAction.CallbackContext obj)
    {
        if (controller.isGrounded && state == CharacterState.Idle)
        {
            Debug.Log("jumping!");
            state = CharacterState.Jumping;
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


    private IEnumerator ShootDelay() {
        yield return new WaitForSeconds(0.25f);
        //xDDD
        luger.localScale = Vector3.one;
        yield return new WaitForSeconds(3.75f);
        luger.localScale = Vector3.zero;
        yield return new WaitForSeconds(1.50f);

        state = CharacterState.Idle;
    }


    // Update is called once per frame
    void Update()
    {
       
        if (state == CharacterState.Jumping && controller.isGrounded && jumpVelocity == 0)
        {
            state = CharacterState.Idle;
            animator.SetBool("jumping", false);
        }

        Vector2 direction = movementAction.action.ReadValue<Vector2>();
        float gravityY = Physics.gravity.y;

        float moveSpeed = state == CharacterState.Crouching ? crouchSpeed : walkSpeed;

        float zMotion = direction.y * moveSpeed;
        float xMotion = direction.x * moveSpeed;
        float yMotion = jumpVelocity + gravityY;


        if (CanWalk())
        {
            controller.Move((transform.forward * zMotion + transform.right * xMotion + transform.up * yMotion) * Time.deltaTime);
            isWalking = Math.Abs(xMotion) > 0 || Math.Abs(zMotion) > 0;
            animator.SetBool("walking", isWalking);
        }else
        {
            isWalking = false;
        }

        jumpVelocity = Mathf.MoveTowards(jumpVelocity, 0, jumpAttentuation * Time.deltaTime);


        HandleMouseLook();
    }


    private bool CanWalk()
    {
        return state != CharacterState.Shooting;
    }

    private void HandleMouseLook()
    {
        Vector2 look = lookAction.action.ReadValue<Vector2>();
        transform.Rotate(Vector3.up, look.x * sensitivity);

        Vector3 newEulerRotation = camPivot.eulerAngles + new Vector3(-look.y * sensitivity, 0f, 0f);
        newEulerRotation.x = ClampAngle(newEulerRotation.x, -90.0f, 90.0f);
        camPivot.eulerAngles = newEulerRotation;

        float scroll = zoomAction.action.ReadValue<Vector2>().y;
        Vector3 pos = cameraTransform.transform.localPosition;
        pos.z += scroll;
        pos.z = Mathf.Clamp(pos.z, -8, -2);
        cameraTransform.transform.localPosition = pos;
    }
}
