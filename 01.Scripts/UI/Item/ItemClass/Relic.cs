using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Relic : Item
{
    public Relic(VisualElement root, RelicSO so) : base(root, so)
    {
    }

    public override void SpriteBoxGet()
    {
        //SpriteBox = Root.Q<VisualElement>("relic_icon_contain-box");
    }

    public override void UseItem()
    {
        ItemManager.Instance.ShowCheckRelicBox(this, new Vector2(682, 35), HandlePositiveEvent, "«ÿ¡¶");
    }

    private void HandlePositiveEvent()
    {
        ItemManager.Instance.CreateItem(CurrentRelicSO);

        ItemManager.Instance.DestoryRelic(this);
    }
}
