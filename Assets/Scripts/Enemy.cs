using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        IdleState();
    }

    public void IdleState()
    {
        if (currentHealth <= 0)
        {
            DeathState();
        }
    }

    public void DeathState()
    {
        Destroy(gameObject);
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
    }
}
