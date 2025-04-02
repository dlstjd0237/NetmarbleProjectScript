using UnityEngine.UIElements;
public class ItemSlot : InventorySlot
{
    public ItemSlot(VisualElement root) : base(root)
    {

    }

    public void ItemTake(ItemSO item)
    {
        _currentItem.CurrentItemSO = item;
    }
    public void ItemTake(RelicSO item)
    {
        _currentItem.CurrentRelicSO = item;
    }

}
