using System.Collections.Generic;

public interface IWeapon {
    List<BaseStat> Stats { get; set; }
    //List<StatBonus> RNDStats { get; set; }
    int CurrentDamage { get; set; }
    void PerformAttack(int damage);
    void PerformSpecialAttack();
}