using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 3;

    [SerializeField]
    private int health;

    private bool isInvincible = false;

    public bool IsInvincible { get { return isInvincible; } set { isInvincible = value; } }

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        throw new NotImplementedException();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Enemy") && (!isInvincible))
        {
            health--;
        }
    }
}
