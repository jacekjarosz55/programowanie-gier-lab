using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class Player : MonoBehaviour
{


    public Animator animator;

#region InputAction
    public InputActionReference lookAction;
    public InputActionReference movementAction;
    public InputActionReference jumpAction;
    public InputActionReference crouchAction;
    public InputActionReference shootAction;
    public InputActionReference aimAction;
    public InputActionReference zoomAction;
    public InputActionReference switchBulletAction;
    public InputActionReference switchFirstPersonViewAction;
    public InputActionReference sprintAction;
    public InputActionReference activateAction;
    public InputActionReference toggleInventoryAction;
    public InputActionReference debugAction;

#endregion


    public UIManager uiManager;
    public List<GameObject> bulletTypes;
    private int currentBullet = 0;

    public Transform shootTransform;

    public int baseAmmo = 30;


    public float sensitivity = 1.0f;
    public float walkSpeed = 1.0f;

    public float crouchSpeedFactor = 0.5f;
    public float aimSpeedFactor = 0.5f;
    public float sprintSpeedFactor = 1.5f;
    public float maxHealth = 100.0f;
    public float health = 100.0f;



    public float jumpForce = 8.0f;
    public float jumpAttentuation = 10.0f;
    private float jumpVelocity = 0.0f;

    private bool isJumping = false;
    private bool isCrouching = false;
    private bool isAiming = false;
    private bool isShooting = false;
    private bool firstPersonView = true;



    public float baseFov = 90;
    public float aimFovDelta = 30;

    public float staminaRechargeMultiplier = 0.25f;
    public float maxStamina = 3.0f;
    public float _stamina = 1.0f;
    public float Stamina
    {
        get => _stamina;
        set
        {
            _stamina = value;
            uiManager.CurrentStamina = value;
        }

    }

    private int _cash;
    public int Cash
    {
        get => _cash;
        set
        {
            _cash = value;
            uiManager.Cash = value;
        }
    }





    private int _ammo;
    public int Ammo { 
        get => _ammo;
        set { 
            _ammo = value;
            uiManager.Ammo = value;
        }
    }

    private float targetFov;


    public List<Item> inventory = new();


    private ItemInteractor interactor;
    private CharacterController controller;
    private Transform camPivot;
    private Transform cameraTransform;
    private List<Renderer> thirdPersonRenderers;
    private List<Renderer> firstPersonRenderers;


    private Camera cam;

    private bool isSprinting = false;
    private bool isWalking = false;


    private IEnumerator HandleShoot()
    {
        Ammo--;
        isShooting = true;
        Instantiate(bulletTypes[currentBullet], shootTransform.position, cameraTransform.rotation, null);

        yield return new WaitForSeconds(1);
        isShooting = false;
        yield return null;
    }

    private void SetupInputActions()
    {
        jumpAction.action.performed += JumpAction_Performed;
        crouchAction.action.started += CrouchAction_Started;
        crouchAction.action.canceled += CrouchAction_Cancelled;
        shootAction.action.started += ShootAction_Started;
        switchBulletAction.action.started += SwitchBulletAction_Started;
        switchFirstPersonViewAction.action.started += SwitchFirstPersonViewAction_Started;
        sprintAction.action.started += SprintActionStarted;
        sprintAction.action.canceled += SprintActionCanceled;
        activateAction.action.started += ActivateAction_Started;
        activateAction.action.canceled += ActivateAction_Canceled;
        aimAction.action.started += _ => StartAiming();
        aimAction.action.canceled += _ => StopAiming();
        toggleInventoryAction.action.started += _ =>
        {
            uiManager.InventoryShown = !uiManager.InventoryShown;
        };
        debugAction.action.performed += _ => ChangeHealth(-1.0f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetupInputActions();

        controller = GetComponent<CharacterController>();
        interactor = GetComponentInChildren<ItemInteractor>();

        camPivot = GameObject.Find("Camera Pivot").transform;
        cameraTransform = GameObject.Find("Main Camera").transform;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        targetFov = baseFov;
        cam.fieldOfView = targetFov;

        thirdPersonRenderers = GameObject.Find("Banana Man").GetComponentsInChildren<Renderer>().ToListPooled();
        firstPersonRenderers = GameObject.Find("Main Camera").GetComponentsInChildren<Renderer>().ToListPooled();

        SetFirstPersonView(true);


        health = maxHealth;
        uiManager.MaxHealth = maxHealth;
        uiManager.CurrentHealth = health;

        uiManager.MaxStamina = maxStamina;
        uiManager.CurrentStamina = _stamina;

        Ammo = baseAmmo;
        uiManager.Ammo = baseAmmo;
        uiManager.Inventory = inventory;
    }

    private void ActivateAction_Started(InputAction.CallbackContext obj)
    {
        interactor.Activate();
        interactor.StartPickup();
    }
    private void ActivateAction_Canceled(InputAction.CallbackContext context)
    {   
        interactor.Deactivate();
        interactor.StopPickup();
    }

    private void StartSprinting()
    {
        StopAiming();
        isSprinting = true;
        animator.SetBool("sprinting", true);
    }

    private void StopSprinting()
    {
        isSprinting = false;
        animator.SetBool("sprinting", false);
    }


    private void SprintActionStarted(InputAction.CallbackContext context)
    {
        StartSprinting();
    }
    private void SprintActionCanceled(InputAction.CallbackContext context)
    {
        StopSprinting();
    }


    private void SetFirstPersonView(bool firstPersonView)
    {
        this.firstPersonView = firstPersonView;
        if (this.firstPersonView)
        {
            cameraTransform.transform.localPosition = Vector3.zero;
            thirdPersonRenderers.ForEach(x => x.enabled = false);
            firstPersonRenderers.ForEach(x => x.enabled = true);
        }
        else
        {
            cameraTransform.transform.localPosition = new Vector3(0, 0, -2);
            thirdPersonRenderers.ForEach(x => x.enabled = true);
            firstPersonRenderers.ForEach(x => x.enabled = false);
        }
    }


    private void SwitchFirstPersonViewAction_Started(InputAction.CallbackContext context)
    {
        SetFirstPersonView(!firstPersonView);
    }

    private void SwitchBulletAction_Started(InputAction.CallbackContext context)
    {
        currentBullet = (currentBullet + 1) % bulletTypes.Count;
    }



    private void StartAiming()
    {
        if (isAiming) return;
        if (isJumping && isCrouching && isWalking) return;
        isAiming = true;
        animator.SetBool("aiming", isAiming);
        targetFov = baseFov - aimFovDelta;
    }

    private void StopAiming()
    {
        if (!isAiming) return;
        isAiming = false;
        animator.SetBool("aiming", isAiming);
        targetFov = baseFov;
    }





    private void ShootAction_Started(InputAction.CallbackContext context)
    {
        if (!isShooting && Ammo > 0)
        {
            StartCoroutine(HandleShoot());

        }
    }

    private void CrouchAction_Started(InputAction.CallbackContext context)
    {
        if (!isShooting && !isJumping && controller.isGrounded)
        {
            StopAiming();
            isCrouching = true;
            animator.SetBool("crouching", true);
        }
    }

    private void CrouchAction_Cancelled(InputAction.CallbackContext context)
    {
        if (isCrouching)
        {
            isCrouching = false;
            animator.SetBool("crouching", false);
        }
    }

    
    private void JumpAction_Performed(InputAction.CallbackContext obj)
    {
        if (controller.isGrounded && !isJumping && !isCrouching && !isShooting && !isAiming)
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

        if (isSprinting)
        {
            Stamina -= Time.deltaTime;
            if (Stamina <= 0)
            {
                StopSprinting();
            }
        }
        else
        {
            Stamina += Time.deltaTime * staminaRechargeMultiplier;
        }
        Stamina = Mathf.Clamp(Stamina, 0, maxStamina);

        Vector2 direction = movementAction.action.ReadValue<Vector2>();
        float gravityY = Physics.gravity.y;
        float moveSpeed = walkSpeed * (isCrouching ? crouchSpeedFactor : 1) * (isAiming ? aimSpeedFactor : 1) * (isSprinting ? sprintSpeedFactor : 1);
        float zMotion = 0;
        float xMotion = 0;

        zMotion = direction.y * moveSpeed;
        xMotion = direction.x * moveSpeed;
        isWalking = Math.Abs(xMotion) > 0 || Math.Abs(zMotion) > 0;
        animator.SetBool("walking", isWalking);

        float yMotion = jumpVelocity + gravityY;
        controller.Move((transform.forward * zMotion + transform.right * xMotion + transform.up * yMotion) * Time.deltaTime);
        jumpVelocity = Mathf.MoveTowards(jumpVelocity, 0, jumpAttentuation * Time.deltaTime);

        Vector2 look = lookAction.action.ReadValue<Vector2>();
        transform.Rotate(Vector3.up, look.x * sensitivity);


        Vector3 newEulerRotation = camPivot.eulerAngles + new Vector3(-look.y * sensitivity, 0f, 0f);
        newEulerRotation.x = ClampAngle(newEulerRotation.x, -90.0f, 90.0f);
        camPivot.eulerAngles = newEulerRotation;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFov, Time.deltaTime*8);

        float scroll = zoomAction.action.ReadValue<Vector2>().y;
        AddZoom(scroll);
    }

    void AddZoom(float zoom)
    {
        if (firstPersonView) return;

        Vector3 pos = cameraTransform.transform.localPosition;
        pos.z += zoom;
        pos.z = Mathf.Clamp(pos.z, -8, -2);
        cameraTransform.transform.localPosition = pos;
    }

    public void ChangeHealth(float value)
    {
        health += value;
        health = Mathf.Clamp(health, 0, maxHealth);
        uiManager.CurrentHealth = health;
    }

    
}
