using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Baek/Item/ItemSO")]
public class ItemSO : InventoryObjectSO
{
    public int AttackF;
    public int AttackP;
    public int DefenseF;
    public int DefenseP;
    public int CriChance;
    public int CriDamage;
    public int Evasion;
    public int Health;
    public int Oxygen;
    public int HealingPercent;
    public ItemElementType ElementType;
    public Dictionary<EntityStatEnum, int> ItemStatDictionary;
    private void OnEnable()
    {
        ItemStatDictionary = new Dictionary<EntityStatEnum, int>();
        ItemStatDictionary.Add(EntityStatEnum.AttackF, AttackF);
        ItemStatDictionary.Add(EntityStatEnum.AttackP, AttackP);
        ItemStatDictionary.Add(EntityStatEnum.DefenseF, DefenseF);
        ItemStatDictionary.Add(EntityStatEnum.DefenseP, DefenseP);
        ItemStatDictionary.Add(EntityStatEnum.CriChance, CriChance);
        ItemStatDictionary.Add(EntityStatEnum.CriDamage, CriDamage);
        ItemStatDictionary.Add(EntityStatEnum.Evasion, Evasion);
        ItemStatDictionary.Add(EntityStatEnum.Health, Health);
        ItemStatDictionary.Add(EntityStatEnum.Oxygen, Oxygen);
        ItemStatDictionary.Add(EntityStatEnum.HealingPercent, HealingPercent);
    }

}
