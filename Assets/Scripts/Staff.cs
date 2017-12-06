﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Staff : MonoBehaviour, IWeapon, IProjectileWeapon
{
    private Animator animator;
    public List<BaseStat> Stats { get; set; }
    public int CurrentDamage { get; set; }
    public Transform ProjectileSpawn{ get; set; }
    Fireball fireball;

    void Start()
    {
        fireball = Resources.Load<Fireball>("Weapons/Projectiles/Fireball");
        animator = GetComponent<Animator>();
    }

    public void PerformAttack(int damage)
    {
        animator.SetTrigger("Base_Attack");
        CastProjectile();
    }

    public void PerformSpecialAttack()
    {
        animator.SetTrigger("Special_Attack");
    }

    public void CastProjectile()
    {
        Debug.Log("cast project");
        Fireball fireballInstance = (Fireball)Instantiate(fireball, ProjectileSpawn.position, ProjectileSpawn.rotation);
        fireballInstance.Direction = ProjectileSpawn.forward;
    }
}
