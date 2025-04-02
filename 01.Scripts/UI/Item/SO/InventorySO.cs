using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Baek/InventorySO")]
public class InventorySO : ScriptableObject
{
    public ItemSO[] itemArr = new ItemSO[16];
    public Dictionary<EquipmentType, ItemSO> equipmentDic;
    private void OnEnable()
    {
        itemArr = new ItemSO[16];
        equipmentDic = new Dictionary<EquipmentType, ItemSO>();
    }


    public void ItemAdd(ItemSO so)
    {
        for (int i = 0; i < itemArr.Length; ++i)
        {
            if (itemArr[i] != null) //이미 아이템이 있을경우
                continue;

            itemArr[i] = so;
            return;
        }
    }

    public void EquipmentAdd(ItemSO so, EquipmentType type)
    {
        equipmentDic.Add(type, so);
    }

    public ItemSO[] GetInventoryItem()
    {
        return itemArr;
    }
    public Dictionary<EquipmentType, ItemSO> GetEquipmentItem()
    {
        return equipmentDic;
    }
}
