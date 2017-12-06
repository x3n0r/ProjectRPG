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
                    Item NowGotItem = ItemDatabase.Instance.GetItem(RolledGearType);

                    //Rolling Rarity
                    ItemRarity = new RareTable();
                    ItemRarity.rarloot = new List<RarityDrop>
                    {
                        //No Common if no roll on Magic or rare or Legendary then it is a Common
                        new RarityDrop(Item.Rarities.Magic, 25),
                        new RarityDrop(Item.Rarities.Rare, 15),
                        new RarityDrop(Item.Rarities.Legendary, 5),
                    };

                    var RolledRarity = ItemRarity.GetRarityDrop();

                    //ItemLevel rewriting it to a curve 
                    //TODO
                    PlayerLevel PlayerLevel = new PlayerLevel();
                    int PlayerLvl = PlayerLevel.Level;
                    Debug.Log("Player LEVEL: " + PlayerLvl);
                    int RolledItemLevel = Random.Range(PlayerLvl - 3, PlayerLvl + 3);

                    //get min max suffix and roll rnd amount of suffixes
                    KeyValuePair<int,int> minmaxsuffix = GetSuffixForRarity(RolledRarity);
                    int minsuffix = minmaxsuffix.Key;
                    int maxsuffix = minmaxsuffix.Value;
                    int rolledSuffixAmount = Random.Range(minsuffix, maxsuffix);
                    List<BaseStat> RolledSuffixStats = new List<BaseStat>();
                    BaseStat.BaseStatType[] allBaseStatTypes = (BaseStat.BaseStatType[])System.Enum.GetValues(typeof(BaseStat.BaseStatType));
                    for (int i = 1; i <= rolledSuffixAmount; i++)
                    {
                        BaseStat.BaseStatType BaseStatType = allBaseStatTypes[Random.Range(0, allBaseStatTypes.Length)];

                        int rolledBaseStatValue = 0;
                        foreach (BaseStat a in NowGotItem.BaseSuffixStats)
                        {
                            
                            if (a.StatType == BaseStatType)
                                rolledBaseStatValue = GetCalculatedMath(a.BaseValue, RolledItemLevel);
                        }
                        
                        BaseStat rolledBaseStat = new BaseStat(BaseStatType, rolledBaseStatValue, "");
                        RolledSuffixStats.Add(rolledBaseStat);
                    }

                    List<BaseStat> CalculatedBaseStats = new List<BaseStat>();
                    foreach (BaseStat a in NowGotItem.BaseSuffixStats)
                    {
                        int calulatedBaseStatValue = GetCalculatedMath(a.BaseValue, RolledItemLevel);
                        BaseStat rolledBaseStat = new BaseStat(a.StatType, calulatedBaseStatValue, a.StatDescription);
                        CalculatedBaseStats.Add(rolledBaseStat);
                    }

                    //Get Some Stuff From Item "Database" (e.g. obect_slug)
                    Item NowRolledItem = new Item(
                        RolledItemType, RolledItemObject, RolledGearType, RolledRarity, 
                        CalculatedBaseStats, new List<BaseStat>(), RolledSuffixStats, RolledItemLevel, 
                        NowGotItem.ObjectSlug, NowGotItem.Description, NowGotItem.ActionName, 
                        NowGotItem.ItemName, NowGotItem.IsStackable
                        );
                    DropItems.Add(NowRolledItem);
                    break;
                }
            }
        }
        return DropItems;
    }

    // boundsmin, boundsmax 
    public KeyValuePair<int, int> GetSuffixForRarity(Item.Rarities rarity)
    {
        if (rarity == Item.Rarities.Magic)
            return new KeyValuePair<int, int>(1, 3);
        else if (rarity == Item.Rarities.Rare)
            return new KeyValuePair<int, int>(2, 4);
        else if (rarity == Item.Rarities.Legendary)
            return new KeyValuePair<int, int>(3, 5);
        else //(rarity == Item.Rarities.Common)
            return new KeyValuePair<int, int>(1, 1);
    }

    public int GetCalculatedMath(int BaseSuffixValue, int ItemLevel )
    {
        //Value* round((Itemlevel+Stage)/ 2)) *rand[0.8, 1.2]
        //
        DungeonStage dungeonStage = new DungeonStage();
        int DungeonLevel = dungeonStage.StageLevel;
        float RND = Random.Range((float)0.8, (float)1.1);
        float b = (float)(BaseSuffixValue * ((ItemLevel + DungeonLevel) / 2)) * RND;
        return (int) b;
    }
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