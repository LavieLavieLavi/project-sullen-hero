using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
  public override State Tick(ZombieManager zombieManager)
    {
        Debug.Log("Attack");
        zombieManager.animator.SetFloat("Vertical", 1, 0.2f, Time.deltaTime);
        return this;
    }


}
