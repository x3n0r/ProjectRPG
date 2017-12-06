﻿using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class ItemDatabase : MonoBehaviour {
    public static ItemDatabase Instance { get; set; }
    private List<Item> Items { get; set; }
	// Use this for initialization
	void Awake () {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        BuildDatabase();
    }
	
    private void BuildDatabase()
    {
        Items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/Items").ToString());
        foreach(Item i in Items)
        {
            if (i.ItemType != Item.ItemTypes.Consumable)
                Debug.Log(i.ItemName + " is a " + i.BaseSuffixStats[5].StatType);
        }
    }

    public Item GetItem(string itemSlug)
    {
        foreach(Item item in Items)
        {
            if (item.ObjectSlug == itemSlug)
                return item;
        }
        Debug.LogWarning("Couldn't find item: " + itemSlug);
        return null;
    }

    public Item GetItem(Item.GearTypes gearType)
    {
        foreach (Item item in Items)
        {
            if (item.GearType == gearType)
                return item;
        }
        Debug.LogWarning("Couldn't find item: " + gearType);
        return null;
    }
}
