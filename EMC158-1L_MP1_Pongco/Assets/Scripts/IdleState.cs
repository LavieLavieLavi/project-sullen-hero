using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{


    //We make our character idle until they find a potential target
    //if a target is fouind we proceed to the pursue target state
    // no target remain in idle
    PursueTargetState pursueTargetState;

    //The layer used to detect potential attack targets
    [Header("Detection Layer")]
    [SerializeField] LayerMask detectionLayer;

    //This Settings determines where our line cast starts on the Y axis of the char (used for LoS)
    [Header("Line of Sight Detection")]
    float charEyeLevel = 1.2f;
    [SerializeField] LayerMask ignoreForLineOfSightDetection;

    [Header("Detection Radius")]
    [SerializeField] float detectionRadius = 5;

    [Header("Detection Angle Radius")]
    [SerializeField] float minDetectionRadiusAngle = -50f;
    [SerializeField] float maxDetectionRadiusAngle = 50f;



    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetState>();
    }


    public override State Tick(ZombieManager zombieManager)
    {
        if (zombieManager.currentTarget != null)
        {
 
            return pursueTargetState;
        }
        else
        {
            FindATargetViaLineOfSight(zombieManager);
            return this;
        }
    }

    private void FindATargetViaLineOfSight(ZombieManager zombieManager)
    {
        //We are searching all colliders on the layer of the player within a certain radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        Debug.Log("We are checking for colliders");

        //For every collider that we find that is on the same layer of the player, we try and search it for a player manager script
        for (int i = 0; i < colliders.Length; i++)
        {
            PlayerManager player = colliders[i].transform.GetComponent<PlayerManager>();


            //if player manager is detected, we check for the line of sight
            if (player != null)
            {

                Debug.Log("we have found the player collider");

                //target must be infront of us
                Vector3 targetDirection = transform.position - player.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > minDetectionRadiusAngle && viewableAngle < maxDetectionRadiusAngle)
                {

                    Debug.Log("we have passed the field of view check");

                    RaycastHit hit;
                    //This just makes it so our raycast does not start from the floor
                    Vector3 playerStartPoint = new  Vector3 (player.transform.position.x,charEyeLevel , player.transform.position.z);
                    Vector3 zombieStartPoint = new Vector3(transform.position.x, charEyeLevel , transform.position.z);

                    Debug.DrawLine(playerStartPoint, zombieStartPoint, Color.white);

                    //Check one last time for object blocking view
                    if (Physics.Linecast(playerStartPoint,zombieStartPoint, out hit, ignoreForLineOfSightDetection))
                    {
                        //Cannot find target, there is an object in the way
                        Debug.Log("There is something in the way");
                    }
                    else
                    {
                        Debug.Log("we have target switching states");
                        zombieManager.currentTarget = player;
                    }
                   
                }

            }

        }

    }




}


