using UnityEngine.UIElements;

public class EquipmentSlot :InventorySlot
{
    public EquipmentSlot(VisualElement root) : base(root)
    {
    }

    public void EquipmentInstallation(ItemSO item)
    {
        _currentItem.CurrentItemSO = item;

    }
}
