using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryItem : Item
{
    public InventoryItem(VisualElement root, ItemSO so) : base(root, so)
    {
    }

    public override void SpriteBoxGet()
    {
        SpriteBox = Root.Q<VisualElement>("item_sprite_contain-box");
    }

    public override void UseItem()
    {
        if (CurrentRelicSO != null)
            ItemManager.Instance.ShowCheckRelicBox(this, new Vector2(30, 35), HandleRelicEvent, "ÀåÂø");
        else
            ItemManager.Instance.ShowCheckBox(this, new Vector2(30, 35), HandlePositiveEvent, "Âø¿ë");

    }
    private void HandlePositiveEvent()
    {
        ItemManager.Instance.CreateEquipment(CurrentItemSO);
        CurrentItemSO = null;
    }
    private void HandleRelicEvent()
    {
        ItemManager.Instance.CreateRelic(CurrentRelicSO);
        CurrentRelicSO = null;
    }
}
