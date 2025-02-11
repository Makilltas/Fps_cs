using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmenyHealth : MonoBehaviour
{
    public int health = 100;
    private int currentHealth;
   private Animator animator;

    void Start()
    {
        currentHealth = health;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        if (currentHealth <= 0) 
        {
            animator.Play("Standing Death Left 01");

        }
    }



}
