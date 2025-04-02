using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventorySlot
{
    protected VisualElement _root;
    protected Item _currentItem;
    public Item CurrentItem => _currentItem;
    public InventorySlot(VisualElement root)
    {
        _root = root;
    }
    public bool IsItemContain()
    {
        if (_currentItem.CurrentItemSO == null && _currentItem.CurrentRelicSO == null)
        {
            return false;
        }
        return true;
    }

    public bool IsRelicContain()
    {
        return _currentItem.CurrentRelicSO != null ? true : false;
    }

    public void SetItemOwner(Item item)
    {
        _currentItem = item;
    }

}
