using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemUtil
{
    public static string GetItemTypeKoreaNmae(ItemType itemType)
    {
        int num = (int)itemType;

        ItemKoreaType koreaItemType = (ItemKoreaType)num;

        return koreaItemType.ToString();
    }
}
