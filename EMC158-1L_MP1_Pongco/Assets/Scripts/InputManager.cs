using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Reference the PlayerControl Script, Locomotion and AnimatorManager
    PlayerControls pControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;
    PlayerAttacker playerAttacker;
    PlayerInventory playerInventory;
    
    public Vector2 movementInput; //Getting the x and y input
    public Vector2 cameraInput; // Getting the x and y of the camera 

    public float cameraInputX; // Getting the camera x input
    public float cameraInputY;// Getting the camera y input


    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool sprintInput;
    public bool jumpInput;
    public bool dodgeInput;

    public bool rbInput;
    public bool rtInput;

    private void Awake()
    {
        animatorManager = FindObjectOfType<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerInventory = GetComponent<PlayerInventory>();
    }
    private void OnEnable()
    {
        if (pControls == null) // meaning not filled in
        {
            pControls = new PlayerControls();

            pControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>(); //Records WASD input

            pControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            pControls.PlayerActions.Sprint.performed += i => sprintInput = true;
            pControls.PlayerActions.Sprint.canceled += i => sprintInput = false;

            pControls.PlayerActions.Jump.performed += i => jumpInput = true;

            pControls.PlayerActions.X.performed += i => dodgeInput = true;

            pControls.PlayerActions.RB.performed += i => rbInput = true;
            pControls.PlayerActions.RT.performed += i => rtInput = true;
         
        }

        pControls.Enable();

    }

    private void OnDisable()
    {
        pControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintInput();
        HandleJumpInput();
        HandleDodgeInput();
        HandleAttackInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;


        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)); // Limiting the value of the movement input so that it will only stay in the range of 0 to 1. Abs converts negative value to positive
        animatorManager.UpdateAnimatorValues(0, moveAmount,playerLocomotion.isSprinting);
    }

    private void HandleSprintInput()
    {
        if (sprintInput && moveAmount>0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            playerLocomotion.HandleJumping();
        }
    }

    private void HandleDodgeInput()
    {
        if (dodgeInput)
        {
            dodgeInput = false;
            playerLocomotion.HandleDodge();
        }
    }


    private void HandleAttackInput ()
    {

        //Right hand weapon light attack
        if (rbInput)
        {
            playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
        }

        if (rtInput)
        {
            playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
        }
    }

}
