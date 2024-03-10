using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator anim;
    PlayerManager playerManager;
    PlayerLocomotion playerLocomotion;
    int horizontal;
    int vertical;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerManager = FindObjectOfType<PlayerManager>(); // this might become a problem because didnt use getcomponent
        playerLocomotion = FindObjectOfType<PlayerLocomotion>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting, bool useRootMotion = false)
    {
        anim.SetBool("isInteracting", isInteracting);
        anim.CrossFade(targetAnimation, 0.2f);

    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovment, bool isSprinting)
    {
        //Animation Snapping
        float snappedHorizontal;
        float snappedVertical;


        #region Snapped horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }

        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if ( horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion
        #region Snapped Vertical
        if (verticalMovment > 0 && verticalMovment < 0.55f)
        {
            snappedVertical = 0.5f;
        }

        else if (verticalMovment > 0.55f)
        {
            snappedVertical = 1;
        }
        else if (verticalMovment < 0 && verticalMovment > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovment < -0.55f)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion

        if (isSprinting)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2;
        }

        anim.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        anim.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        if (playerManager.isUsingRootMotion)
        {
            playerLocomotion.rb.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / Time.deltaTime;
            playerLocomotion.rb.velocity = velocity;
        }
    }
}
