using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage = 25;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();
        //if the object that it collided with has a player stats script attach to it this if statement will trigger.

        if(playerStats != null) // if player touches the cylinder
        {
            playerStats.TakeDamage(damage);
        }
    }



  
}
