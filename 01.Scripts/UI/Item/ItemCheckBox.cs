using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection;
using System;

public class ItemCheckBox : MonoBehaviour
{
    [SerializeField] private UIDocument _doc;
    [SerializeField] private VisualTreeAsset _itemStatInfo;

    private VisualElement _root;
    private VisualElement _itemInfoBox;
    private VisualElement _spriteContain;

    private Dictionary<EquipmentType, VisualElement> _equipmentDictionary;

    private Label _itemNameLabel;
    private Label _itemTypeLabel;

    private Button _positiveBtn;
    private Button _cancelBtn;

    private List<Label> _statLabelList;


    private Action _positiveEvent = null;
    private void Awake()
    {
        ItemManager.Instance.SetCheckBoxOwner(this);

        _doc = _doc == null ? GetComponent<UIDocument>() : _doc; //만약 UI가 읎다면 
    }

    private void OnEnable()
    {
        _statLabelList = new List<Label>();
        _equipmentDictionary = new Dictionary<EquipmentType, VisualElement>();
        _root = _doc.rootVisualElement;

        var statLabelContin = _root.Q<VisualElement>("item_info_contain-box");
        _itemInfoBox = _root.Q<VisualElement>("item_check_contain-box");
        _spriteContain = _root.Q<VisualElement>("item_check_sprite_contain-box");
        _itemNameLabel = _root.Q<Label>("item_name-label");
        _itemTypeLabel = _root.Q<Label>("item_type-label");

        _positiveBtn = _root.Q<Button>("item_check_positive-btn");
        _cancelBtn = _root.Q<Button>("item_check_cancel-btn");

        _positiveBtn.RegisterCallback<ClickEvent>(HandlePositiveEvent);
        _cancelBtn.RegisterCallback<ClickEvent>(HandleCancelEvent);

        foreach (EquipmentType item in Enum.GetValues(typeof(EquipmentType)))
        {
            _equipmentDictionary.Add(item, _root.Q<VisualElement>($"equipment_background_{item.ToString().ToLower()}_icon_contain-box"));
        }

        for (int i = 0; i < 10; ++i)
        {
            var statLabel = _itemStatInfo.Instantiate().Q<Label>();
            _statLabelList.Add(statLabel);
            statLabelContin.Add(statLabel);
        }
        HideItemInfoBox();
    }

    private void HandleCancelEvent(ClickEvent evt)
    {
        HideItemInfoBox();

    }

    private void HandlePositiveEvent(ClickEvent evt)
    {
        _positiveEvent?.Invoke();
        HideItemInfoBox();
    }


    public void ShowRelicInfoBox(Item item, Vector2 pos, Action PositiveEvent, string positivStr)
    {
        _itemInfoBox.RemoveFromClassList("on");
        _itemInfoBox.style.left = pos.x;
        _itemInfoBox.style.top = pos.y;
        _positiveEvent = PositiveEvent;
        _positiveBtn.text = positivStr;

        _spriteContain.style.backgroundImage = new StyleBackground(item.Itemimage);
        _itemNameLabel.text = item.ItemName;
        _itemTypeLabel.text = ItemUtil.GetItemTypeKoreaNmae(item.ItemType);

    }
    public void ShowItemInfoBox(Item item, Vector2 pos, Action PositiveEvent, string positivStr)
    {
        _itemInfoBox.RemoveFromClassList("on");
        _itemInfoBox.style.left = pos.x;
        _itemInfoBox.style.top = pos.y;
        _positiveEvent = PositiveEvent;
        _positiveBtn.text = positivStr;
        ItemSO itemSO = item.CurrentItemSO;

        _spriteContain.style.backgroundImage = new StyleBackground(item.Itemimage);
        _itemNameLabel.text = item.ItemName;
        _itemTypeLabel.text = ItemUtil.GetItemTypeKoreaNmae(item.ItemType);

        int count = 0;
        foreach (var value in Enum.GetValues(typeof(EntityStatEnum)))
        {
            Type t = typeof(ItemSO);
            FieldInfo field = t.GetField(value.ToString());
            int itemValue = (int)field.GetValue(itemSO);
            if (itemValue != 0)
            {
                _statLabelList[count].style.display = DisplayStyle.Flex;
                _statLabelList[count].style.display = DisplayStyle.Flex;
                _statLabelList[count].text = $"{value} : {itemValue}";
                count++;
            }
        }
    }

    private void HideItemInfoBox()
    {
        _itemInfoBox.AddToClassList("on");

        for (int i = 0; i < 10; ++i)
        {
            _statLabelList[i].style.display = DisplayStyle.None;
        }
    }

}
