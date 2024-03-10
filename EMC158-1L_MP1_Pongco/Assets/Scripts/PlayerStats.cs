using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerStats : MonoBehaviour
{
    public int healthLevel = 10; // to set the maxHealth
    public int maxHealth; 
    public int currentHealth;


    public HealthBar healthbar;

    AnimatorManager animatorManager;

    private void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthbar.SetMaxhHealth(maxHealth);
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        healthbar.SetCurrentHealth(currentHealth);
        animatorManager.PlayTargetAnimation("Damage_01", true);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animatorManager.PlayTargetAnimation("Dead_01", true);
            //Handle player death
            Invoke(nameof(ReloadLevel), 1.3f);
        }

    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
