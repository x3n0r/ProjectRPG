﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public CharacterStats characterStats;
    public int currentHealth;
    public int maxHealth;
    public PlayerLevel PlayerLevel { get; set; }
    public DungeonStage DungeonStage { get; set; }

    void Start()
    {
        PlayerLevel = GetComponent<PlayerLevel>();
        this.currentHealth = this.maxHealth;
        characterStats = new CharacterStats(0, 1, 5, 10, 10, 5);
    }


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }

    private void Die()
    {
        Debug.Log("Player dead. Reset health.");
        this.currentHealth = this.maxHealth;
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }
}
