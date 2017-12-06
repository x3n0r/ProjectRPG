using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable
{
    public RareTable ItemRarity { get; set; }
    public List<LootDrop> loot;

    public List<Item> GetEquipDrop(int ItemAmount)
    {
        List<Item> DropItems = new List<Item>();
        int weightSum = 0;
        var RolledItemType = Item.ItemTypes.Gear;
        for (int amount = 1; amount <= ItemAmount; amount++)
        {
            //Rolling ItemObject
            int roll = Random.Range(0, 101);
            foreach (LootDrop drop in loot)
            {
                weightSum += drop.Weight;
                if (roll < weightSum)
                {
                    //Rolled ItemObject
                    var RolledItemObject = drop.ItemObject;

                    //Rolling GearType Random No. from list
                    Item.ItemObjects type = drop.ItemObject;
                    var RolledGearType = Item.itemToGearTypes[type][Random.Range(0, Item.itemToGearTypes[type].Count)];

                    //Get Some Stuff From Item "Database" (e.g. obect_slug)
                    Item NowRolledItem = ItemDatabase.Instance.GetItem(RolledGearType);
                    
                    //Set already Rolled variables
                    NowRolledItem.ItemObject = RolledItemObject;
                    NowRolledItem.ItemType = RolledItemType;

                    //Rolling Rarity
                    //Item.Rarities[] Irar = (Item.Rarities[])System.Enum.GetValues(typeof(Item.Rarities));
                    //NowRolledItem.Rarity = Irar[Random.Range(0, Irar.Length)];


                    ItemRarity = new RareTable();
                    ItemRarity.rarloot = new List<RarityDrop>
                    {
                        //No Common if no roll on Magic or rare or Legendary then it is a Common
                        new RarityDrop(Item.Rarities.Magic, 25),
                        new RarityDrop(Item.Rarities.Rare, 15),
                        new RarityDrop(Item.Rarities.Legendary, 5),
                    };

                    var rolledrar = ItemRarity.GetRarityDrop();
                    NowRolledItem.Rarity = rolledrar;


                        //int rarroll = Random.Range(0, 101);
                        //foreach (RarityDrop rardrop in ItemRarity)
                        //{
                        //    weightSum += rardrop.Weight;
                        //    if (roll < weightSum)
                        //    {

                        //        break;
                        //    }
                        //}
                        //

                        //

                        // (USERTYPE[])Enum.GetValues(typeof(USERTYPE));
                        //Enum.GetValues(typeof(USERTYPE)).Cast<USERTYPE>().ToList();
                    break;
                    //Item a = ItemDatabase.Instance.GetItem(drop.ItemSlug);
                    //a.Rarity = Item.Rarities.Normal;
                    //return DropItems.Add(ItemDatabase.Instance.GetItem(drop.ItemSlug));
                }
            }
        }
        return DropItems;
    }
}

public boundslow, boundsmax GetSuffixForRarity(Item.Rarities rarity)
{
    var list = new List<System.Tuple<int, int>>();


}

public class RareTable
{
    public List<RarityDrop> rarloot;

    public Item.Rarities GetRarityDrop()
    {
        List<Item> DropItems = new List<Item>();
        int weightSum = 0;
        //Rolling ItemObject
        int roll = Random.Range(0, 101);
        foreach (RarityDrop rardrop in rarloot)
        {
            weightSum += rardrop.Weight;
            if (roll < weightSum)
            {
                return rardrop.Rarity;
            }
        }
        return Item.Rarities.Common;
    }
}

public class LootDrop
{
    public Item.ItemObjects ItemObject { get; set; }
    public int Weight { get; set; }

    public LootDrop(Item.ItemObjects _ItemObject, int _Weight)
    {
        this.ItemObject = _ItemObject;
        this.Weight = _Weight;
    }
}

public class RarityDrop
{
    public Item.Rarities Rarity { get; set; }
    public int Weight { get; set; }

    public RarityDrop(Item.Rarities _Rarity, int _Weight)
    {
        this.Rarity = _Rarity;
        this.Weight = _Weight;
    }
}