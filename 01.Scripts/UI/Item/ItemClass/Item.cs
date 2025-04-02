using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public abstract class Item
{
    public string ItemName;
    public Sprite Itemimage;
    public ItemType ItemType;
    public VisualElement Root;
    private VisualElement _spriteBox;
    public VisualElement SpriteBox { get => _spriteBox; set => _spriteBox = value; }
    private Action<ItemSO> _itemChangeEvent = null;
    private ItemSO _currentItemSO = null;
    public ItemSO CurrentItemSO
    {
        get => _currentItemSO;
        set
        {
            _currentItemSO = value;
            _itemChangeEvent?.Invoke(_currentItemSO);
        }
    }
    private Action<RelicSO> _relicChangeEvent = null;

    private RelicSO _currentRelicSO = null;
    public RelicSO CurrentRelicSO
    {
        get => _currentRelicSO;
        set
        {
            _currentRelicSO = value;
            _relicChangeEvent?.Invoke(_currentRelicSO);
        }
    }

    public Item(VisualElement root, ItemSO so)
    {
        Root = root;
        SpriteBoxGet();
        _itemChangeEvent += HandleItemChangeEvent;
        _relicChangeEvent += HandleRelicChangeEvent;

        if (so != null)
        {
            _currentItemSO = so;
            ItemName = so.ItemName;
            Itemimage = so.ItemSprite;
            ItemType = so.ItemType;
            _spriteBox.style.backgroundImage = new StyleBackground(Itemimage);
            _spriteBox.style.flexGrow = 1;
            _spriteBox.style.alignItems = Align.Center;
        }
        Root.RegisterCallback<ClickEvent>(evt =>
        {
            if (_currentItemSO != null || _currentRelicSO != null)
            {

                UseItem();
                //So가 들었을떄 아이템 사용 또는 장착이 떠야함
            }
            //만약 if에 안걸러지면 해당 슬롯에 Item이 없는거다.
        });
    }
    public Item(VisualElement root, RelicSO so)
    {
        Root = root;
        SpriteBoxGet();
        _itemChangeEvent += HandleItemChangeEvent;
        _relicChangeEvent += HandleRelicChangeEvent;
        if (so != null)
        {
            _currentRelicSO = so;
            ItemName = so.ItemName;
            Itemimage = so.ItemSprite;
            ItemType = so.ItemType;
            _spriteBox.style.backgroundImage = new StyleBackground(Itemimage);
            _spriteBox.style.flexGrow = 1;
            _spriteBox.style.alignItems = Align.Center;
        }
        Root.RegisterCallback<ClickEvent>(evt =>
        {
            if (_currentItemSO != null || _currentRelicSO != null)
            {

                UseItem();
                //So가 들었을떄 아이템 사용 또는 장착이 떠야함
            }
            else
            {
                Debug.Log("아무것도 안눌리노");
            }
        });
    }



    ~Item()
    {
        if (_itemChangeEvent != null)
            _itemChangeEvent -= HandleItemChangeEvent;
        if (_relicChangeEvent != null)
            _relicChangeEvent -= HandleRelicChangeEvent;
    }
    private void HandleRelicChangeEvent(RelicSO so)
    {
        Debug.Log("그럼 이것도 되냐?");
        if (_spriteBox == null)
        {
            _spriteBox = Root.Q<VisualElement>("item_sprite_contain-box");
            _spriteBox.style.flexGrow = 1;
            _spriteBox.style.alignItems = Align.Center;
        }
        if (so == null)
        {

            ItemName = null;
            Itemimage = null;
            ItemType = ItemType.Armor;
            _spriteBox.style.backgroundImage = null;
            return;
        }
        ItemName = so.ItemName;
        Itemimage = so.ItemSprite;
        ItemType = so.ItemType;
        _spriteBox.style.backgroundImage = new StyleBackground(Itemimage);
        _spriteBox.style.unityBackgroundImageTintColor = new StyleColor(Color.white);
    }

    private void HandleItemChangeEvent(ItemSO so)
    {
        if (_spriteBox == null)
        {
            _spriteBox = Root.Q<VisualElement>("item_sprite_contain-box");
            _spriteBox.style.flexGrow = 1;
            _spriteBox.style.alignItems = Align.Center;
        }

        if (so == null)
        {

            ItemName = null;
            Itemimage = null;
            ItemType = ItemType.Armor;
            _spriteBox.style.backgroundImage = null;
            return;
        }
        ItemName = so.ItemName;
        Itemimage = so.ItemSprite;
        ItemType = so.ItemType;
        _spriteBox.style.backgroundImage = new StyleBackground(Itemimage);
        _spriteBox.style.unityBackgroundImageTintColor = new StyleColor(Color.white);
    }


    public abstract void UseItem();
    public abstract void SpriteBoxGet();
}
