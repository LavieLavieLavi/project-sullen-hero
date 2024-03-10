using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator anim;

    InputManager inputManager;
    CameraManager cameraManager;
    PlayerLocomotion playerLocomotion;

    public bool isInteracting;
    public bool isUsingRootMotion;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }




    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();

        isInteracting = anim.GetBool("isInteracting");
        isUsingRootMotion = anim.GetBool("isUsingRootMotion");
        playerLocomotion.isJumping = anim.GetBool("isJumping");
        anim.SetBool("isGrounded", playerLocomotion.isGrounded);

        inputManager.rtInput = false;
        inputManager.rbInput = false;
    }

}
