using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
public class Item {

    public static Dictionary<ItemObjects, List<GearTypes>> itemToGearTypes;
    static Item() {
        itemToGearTypes = new Dictionary<ItemObjects, List<GearTypes>>();
        itemToGearTypes.Add(ItemObjects.Armor, new List<GearTypes> { GearTypes.Armor, GearTypes.Gloves, GearTypes.Helmet, GearTypes.Pants, GearTypes.Shoes });
        itemToGearTypes.Add(ItemObjects.Jewelery, new List<GearTypes> { GearTypes.Amulett, GearTypes.Ring});
        itemToGearTypes.Add(ItemObjects.Weapon, new List<GearTypes> { GearTypes.Sword, GearTypes.Staff, GearTypes.Crossbow });
    }

    public enum ItemTypes { Gear, Consumable, Quest }
    public enum ItemObjects { Jewelery, Armor, Weapon }
    public enum GearTypes { Ring, Amulett, Helmet, Armor, Gloves, Pants, Shoes, Sword, Staff, Crossbow}
    public enum Rarities { Common, Magic, Rare , Legendary  }

    public List<BaseStat> RndSuffixStats { get; set; }
    public int ItemLevel { get; set; }
    public Rarities Rarity { get; set; }

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public ItemTypes ItemType { get; set; }
    public ItemObjects ItemObject { get; set; }
    public GearTypes GearType { get; set; }
    public List<BaseStat> BaseStats { get; set; }
    public List<BaseStat> BaseSuffixStats { get; set; }
    public string ObjectSlug { get; set; }
    public string Description { get; set; }
    public string ActionName { get; set; }
    public string ItemName { get; set; }
    public bool IsStackable { get; set; }

    public Item(List<BaseStat> _BaseStats, string _ObjectSlug)
    {
        this.BaseStats = _BaseStats;
        this.ObjectSlug = _ObjectSlug;
    }

    [JsonConstructor]
    public Item(
        ItemTypes _ItemType,
        ItemObjects _ItemObject,
        GearTypes _GearType,
        Rarities _Rarity,
        List<BaseStat> _BaseStats,
        List<BaseStat> _BaseSuffixStats,
        List<BaseStat> _RndSuffixStats,
        int _ItemLevel,
        string _ObjectSlug,
        string _Description,
        string _ActionName, 
        string _ItemName,
        bool _IsStackable
        )
    {
        this.BaseStats = _BaseStats;
        this.RndSuffixStats = _RndSuffixStats;
        this.ObjectSlug = _ObjectSlug;
        this.Description = _Description;
        this.Rarity = _Rarity;
        this.ItemObject = _ItemObject;
        this.ItemType = _ItemType;
        this.ActionName = _ActionName;
        this.ItemName = _ItemName;
        this.IsStackable = _IsStackable;
        itemToGearTypes = new Dictionary<ItemObjects, List<GearTypes>>();
        itemToGearTypes.Add(ItemObjects.Armor, new List<GearTypes> { GearTypes.Armor, GearTypes.Gloves, GearTypes.Helmet, GearTypes.Pants, GearTypes.Shoes });
        itemToGearTypes.Add(ItemObjects.Jewelery, new List<GearTypes> { GearTypes.Armor, GearTypes.Gloves, GearTypes.Helmet, GearTypes.Pants, GearTypes.Shoes });
        itemToGearTypes.Add(ItemObjects.Weapon, new List<GearTypes> { GearTypes.Armor, GearTypes.Gloves, GearTypes.Helmet, GearTypes.Pants, GearTypes.Shoes });
    }
}

//bitwise operation Rarities to Suffix Amount
//public enum Rarities { Common=1|(1 << 4), Magic= 1 | (3 << 4), Rare= 2 | (4 << 4), Legendary= 3 | (5 << 4) }
//    int ind = lowerBound | (upperBound << 16);
//    lowerBound = ind & Short.MAX_VALUE;
//upperBound = ind >> 16;

//bounds <= 00001111; (15)
//lowerBound = 1; -> 00000001
//upperBound = 3; -> 00000011
//oneNumber = lowerBound | (upperBound << 4)
//lowerBound = 				00000001
//upperBound << 4 = 			00110000
//lowerBound | upperBound =	00110001

//oneNumber =			00110001
//15 		  = 		00001111
//oneNumber & 15 =	00000001
//oneNumber >> 4 = 	00110001
//					00000011
//lowerBound = oneNumber & 15 = 0000001
//int something = (int)Question.Role;