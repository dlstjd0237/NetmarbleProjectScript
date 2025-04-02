using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Equipment : Item
{
    public Equipment(VisualElement root, ItemSO so) : base(root, so)
    {
        Root = root;
    }

    public override void SpriteBoxGet()
    {
        SpriteBox = Root.Q<VisualElement>("equpment_sprite_contain-box");
    }

    public override void UseItem()
    {
        
        ItemManager.Instance.ShowCheckBox(this, new Vector2(682, 35), HandlePositiveEvent, "Å»ÀÇ");
    }

    private void HandlePositiveEvent()
    {
        ItemManager.Instance.CreateItem(CurrentItemSO);

        ItemManager.Instance.DestoryEquipment(this);
    }
}
