using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RelicSlot : InventorySlot
{
    public RelicSlot(VisualElement root) : base(root)
    {

    }
    public void ItemTake(RelicSO item)
    {
        _currentItem.CurrentRelicSO = item;
    }


}
