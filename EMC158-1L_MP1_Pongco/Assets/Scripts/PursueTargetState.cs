using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTargetState :State
{
    public AttackState attackState;

    private void Awake()
    {
        attackState = GetComponent<AttackState>();
    }

    public override State Tick(ZombieManager zombieManager)
    {
        Debug.Log("pursue target");
        MoveTowardsCurrentTarget(zombieManager);
        RotateTowardsTarget(zombieManager);


        if (zombieManager.distanceFromCurrentTarget <= zombieManager.minimumAttackDistance)
        {
            Debug.Log("Switching to attacking");
            return attackState;
        }
        else
        {
            return this;
        }
     
        
    }

    /*   private void MoveTowardsCurrentTarget(ZombieManager zombieManager)
       {
           //Enable movement via animation blend tree
           zombieManager.animator.SetFloat("Vertical", 1, 0.2f, Time.deltaTime);

       }*/

    private void MoveTowardsCurrentTarget(ZombieManager zombieManager)
    {
        PlayerManager player = FindObjectOfType<PlayerManager>();

        if (player != null)
        {
            Debug.Log("Found a target");

            // Calculate the direction from the zombie to the player
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            // Set the zombie's forward direction to face the player
            zombieManager.transform.forward = directionToPlayer;

            // Enable movement via animation blend tree
            zombieManager.animator.SetFloat("Vertical", 1, 0.2f, Time.deltaTime);

            // Smoothly move the zombie towards the player's position
            float moveSpeed = .5f;
            Vector3 targetPosition = transform.position + directionToPlayer * moveSpeed;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            zombieManager.zombieRB.MovePosition(newPosition);
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }
    }

    private void RotateTowardsTarget(ZombieManager zombieManager)
    {
        zombieManager.zombieNavmeshAgent.enabled = true;
        zombieManager.zombieNavmeshAgent.SetDestination(zombieManager.currentTarget.transform.position);
        zombieManager.transform.rotation = Quaternion.Slerp(zombieManager.transform.rotation, zombieManager.zombieNavmeshAgent.transform.rotation, zombieManager.rotationSpeed / Time.deltaTime);
    }
}
