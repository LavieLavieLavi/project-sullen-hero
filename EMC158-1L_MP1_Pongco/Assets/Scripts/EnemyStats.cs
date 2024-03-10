using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int healthLevel = 10; // to set the maxHealth
    public int maxHealth;
    public int currentHealth;


    public float delayTime = 2.0f;


    Animator anim;
    ZombieManager zombieManager;
 

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        zombieManager = GetComponent<ZombieManager>();
    
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
  
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    IEnumerator DestroyAfterAnimation()
    {
        anim.Play("Dead_01"); // Replace "YourAnimationName" with the actual animation state name

        // Wait for the animation to finish
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        // Add additional delay if needed
        yield return new WaitForSeconds(delayTime);

        
        // Destroy the object
        Destroy(gameObject);
    }


    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            zombieManager.enabled = false;
            StartCoroutine(DestroyAfterAnimation());
            //Handle player death
        }

    }
}
