using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
public class Item {
    public enum ItemTypes { Consumable, Weapon, Quest }
    public enum ItemObjects { Jewelery, Armor, Weapon }
    public enum Rarities { Normal, Rare, Epic, Mythic, Legendary }
    public List<BaseStat> BaseStats { get; set; }
    public List<BaseStat> BaseRNDStats { get; set; }
    public string ObjectSlug { get; set; }
    public string Description { get; set; }
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public Rarities Rarity { get; set; }
    public ItemObjects ItemObject { get; set; }
    public ItemTypes ItemType { get; set; }
    public string ActionName { get; set; }
    public string ItemName { get; set; }
    public bool Identified { get; set; }

    public Item(List<BaseStat> _BaseStats, string _ObjectSlug)
    {
        this.BaseStats = _BaseStats;
        this.ObjectSlug = _ObjectSlug;
    }

    [JsonConstructor]
    public Item(List<BaseStat> _BaseStats, List<BaseStat> _BaseRNDStats, string _ObjectSlug, string _Description, Rarities _Rarity, ItemObjects _ItemObject, ItemTypes _ItemType, string _ActionName, string _ItemName, bool _Identified)
    {
        this.BaseStats = _BaseStats;
        this.BaseRNDStats = _BaseRNDStats;
        this.ObjectSlug = _ObjectSlug;
        this.Description = _Description;
        this.Rarity = _Rarity;
        this.ItemObject = _ItemObject;
        this.ItemType = _ItemType;
        this.ActionName = _ActionName;
        this.ItemName = _ItemName;
        this.Identified = _Identified;
    }
}
