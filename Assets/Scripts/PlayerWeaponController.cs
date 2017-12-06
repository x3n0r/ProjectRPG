﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeaponController : MonoBehaviour {
    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }

    Transform spawnProjectile;
    Item currentlyEquippedItem;
    IWeapon equippedWeapon;
    CharacterStats characterStats;

    void Start()
    {
        spawnProjectile = transform.Find("ProjectileSpawn");
        characterStats = GetComponent<Player>().characterStats;
    }

    public void EquipWeapon(Item itemToEquip)
    {
        if (EquippedWeapon != null)
        {
            UnequipWeapon();
        }

        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug), 
            playerHand.transform);
        equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
        if (EquippedWeapon.GetComponent<IProjectileWeapon>() != null)
            EquippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile;
        //EquippedWeapon.transform.SetParent(playerHand.transform);
        equippedWeapon.Stats = itemToEquip.BaseStats;
        currentlyEquippedItem = itemToEquip;
        characterStats.AddStatBonus(itemToEquip.BaseStats);
        if (itemToEquip.RndSuffixStats != null)
            characterStats.AddStatBonus(itemToEquip.RndSuffixStats);
        UIEventHandler.ItemEquipped(itemToEquip);
        UIEventHandler.StatsChanged();
    }

    public void UnequipWeapon()
    {
        InventoryController.Instance.GiveItem(currentlyEquippedItem.ObjectSlug);
        characterStats.RemoveStatBonus(currentlyEquippedItem.BaseStats);
        if (currentlyEquippedItem.RndSuffixStats != null)
            characterStats.RemoveStatBonus(currentlyEquippedItem.RndSuffixStats);
        Destroy(EquippedWeapon.transform.gameObject);
        UIEventHandler.StatsChanged();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            PerformWeaponAttack();
        if (Input.GetKeyDown(KeyCode.Z))
            PerformWeaponSpecialAttack();
        if (Input.GetKeyDown(KeyCode.V))
        {
            List<BaseStat> t = new List<BaseStat>();
            t = new List<BaseStat>() {
                new BaseStat(BaseStat.BaseStatType.Power, 7, "Power")
            };
            Item i = new Item(t, "sword");
            EquipWeapon(i);
        }

    }

    public void PerformWeaponAttack()
    {
        if (equippedWeapon != null)
            equippedWeapon.PerformAttack(CalculateDamage());
        else
            Debug.Log("No Weapon");
    }
    public void PerformWeaponSpecialAttack()
    {
        if (equippedWeapon != null)
            equippedWeapon.PerformSpecialAttack();
        else
            Debug.Log("No Weapon");
    }

    private int CalculateDamage()
    {
        int damageToDeal = (characterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue() * 2)
            + Random.Range(2, 8);
        damageToDeal += CalculateCrit(damageToDeal);
        Debug.Log("Damage dealt: " + damageToDeal);
        return damageToDeal;
    }

    private int CalculateCrit(int damage)
    {
        if (Random.value <= .10f)
        {
            int critDamage = (int)(damage * Random.Range(.5f, .75f));
            return critDamage;
        }
        return 0;
    }
}
