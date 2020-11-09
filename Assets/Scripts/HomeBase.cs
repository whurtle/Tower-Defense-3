using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HomeBase : MonoBehaviour
{
    public int currentHealth;
    private int startingHealth;
    public HealthBar healthBar;
    public GameState gameState;


    public void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        startingHealth = currentHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            this.TakeDamage(other.GetComponent<Enemy>().currentHealth);
        }
    }

    public void TakeDamage(int amountDamage)
    {
        currentHealth -= amountDamage;
        if (currentHealth < 0)
        {
            Destroy(this.gameObject);
            gameState.ShowRestart();
        }
        else
        {
            healthBar.Damage(currentHealth, startingHealth);
        }
    }

    public void Restart()
    {
        this.currentHealth = startingHealth;
    }


}
