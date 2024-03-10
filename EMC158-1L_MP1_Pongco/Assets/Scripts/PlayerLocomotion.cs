using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;
    AnimatorManager animatorManager;


   
    Vector3 moveDirection;
    Transform cameraObject;
    public Rigidbody rb;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffset = 0.5f;
    public LayerMask groundLayer;



    [Header("Movement Flags")]
    public bool isGrounded;
    public bool isSprinting;
    public bool isJumping;

    [Header ("Movement Speeds")]
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 5;
    public float sprintingSpeed = 7;
    public float rotationSpeed = 15;

    [Header("Jump Speed")]
    public float gravityIntensity = -15;
    public float jumpHeight = 3;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        playerManager = GetComponent<PlayerManager>();
        animatorManager = FindObjectOfType<AnimatorManager>();
    }

    public void HandleAllMovement()
    {
        //Above because no matter what actions if the player falls off a cliff they will fall no matter what they are doing
        HandleFallingandLanding();
        if (playerManager.isInteracting)
            return;
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement() //Handles the movement of the player
    {
        if (isJumping)
            return;


        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize(); //keeps the movement consistent
        moveDirection.y = 0;

        if (isSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * runningSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkingSpeed;
            }
        }

        // if we are sprint, select spritn speed
        //if running , select walking speed
        //if walking, select walking speed
        Vector3 movementVelocity = moveDirection;
        rb.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        if (isJumping)
            return;

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;


        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation,targetRotation, rotationSpeed * Time.deltaTime); //Always same speed no matter the frame rate.

        transform.rotation = playerRotation;

    }

    private void HandleFallingandLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;
        targetPosition = transform.position;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("JumpAir_Normal_InPlace_SwordAndShield", true);
            }

            animatorManager.anim.SetBool("isUsingRootMotion", false);
            inAirTimer = inAirTimer + Time.deltaTime;
            rb.AddForce(transform.forward * leapingVelocity);
            rb.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if (!isGrounded && !playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("JumpEnd_Normal_InPlace_SwordAndShield", true);
            }
            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;
            inAirTimer = 0;
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }

        if(isGrounded && !isJumping)
        {
            if (playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }

        if (playerManager.isInteracting || inputManager.moveAmount > 0)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
        }
        else
        {
            transform.position = targetPosition;
        }

    }


    public void HandleJumping()
    {
        if (isGrounded)
        {
            animatorManager.anim.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("JumpFull_Spin_RM_SwordAndShield", false);

            float jumpVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection; // to get the velocity on forward
            playerVelocity.y = jumpVelocity; // getting the velocity and to added your velocity while walking or running to the jumping velocity so the player can get propelled farther
            rb.velocity = playerVelocity; // setting the rigidbody velocity

        }
    }


    public void HandleDodge()
    {
        if (playerManager.isInteracting)
            return;

        animatorManager.PlayTargetAnimation("Dodge", true,true);
        //Toggle invulnerable



    }


}
