using System;
using UnityEngine;

[Serializable]
public class Stat_OffenseGroup
{
    public Stat attackSpeed;
    //physical
    public Stat damage;
    public Stat critPower;
    public Stat critChance;
    public Stat armorReduction;
    //elemental
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
}
