using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpsHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
    }

    private void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            Debug.Log("Caným azaldý! Þu anki caným: " + currentHealth);
        }


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Nps öldü!");
    }
}
