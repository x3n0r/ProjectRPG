using UnityEngine;
using System.Collections.Generic;

public class CharacterStats {
    public List<BaseStat> stats = new List<BaseStat>();

    public CharacterStats(int mindamage, int maxdamage, int attackSpeed, int power, int vitality, int intelligence)
    {
        stats = new List<BaseStat>() {
            new BaseStat(BaseStat.BaseStatType.MinDamage, mindamage, "min dmg"),
            new BaseStat(BaseStat.BaseStatType.MaxDamage, maxdamage, "max dmg"),
            new BaseStat(BaseStat.BaseStatType.AttackSpeed, attackSpeed, "Atk Spd"),
            new BaseStat(BaseStat.BaseStatType.Power, power, "pwr"),
            new BaseStat(BaseStat.BaseStatType.Vitality, vitality, "vit"),
            new BaseStat(BaseStat.BaseStatType.Intelligence, intelligence, "int")
        };
    }

    public BaseStat GetStat(BaseStat.BaseStatType stat)
    {
        return this.stats.Find(x => x.StatType == stat);
    }

    public void AddStatBonus(List<BaseStat> statBonuses)
    {
        foreach(BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).AddStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }

    public void RemoveStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }
}
