using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection;
using DG.Tweening;
using UAPT.Utile;
public class InventoryUI : MonoBehaviour
{
    private readonly StyleColor _noneColor = new StyleColor(new Color(0, 0, 0, 0.5f));
    private readonly short _itemSlotSize = 16; //인벤토리 아이템창 최대 갯수

    [SerializeField] private VisualTreeAsset _spriteIcon;
    [SerializeField] private EquipmentSpriteListSO _equipmentListSO;
    [SerializeField] private InventorySO _inventorySO;



    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _inventoryRoot;


    private Dictionary<TabbarType, Button> _tabbarContainDictionary; // 텝바 컨테인
    private Dictionary<TabbarType, VisualElement> _pageContainDictionary; //인벤토리 페이지 컨테인
    private Dictionary<int, ItemSlot> _itemSlotDictionary; //아이템창 슬롯
    private Dictionary<int, RelicSlot> _relicslotDictionary; //유물 슬롯
    private Dictionary<EquipmentType, EquipmentSlot> _equipmentSlotDictionary; //장비창 슬롯
    private Dictionary<EquipmentType, Sprite> _equipmentDefaultSpriteDictionary;//장비창 기본 이미지
    private Dictionary<EntityStatEnum, Label> _statLabelDictionary; //스텟 레이블 



    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        _root = _doc.rootVisualElement;
        _inventoryRoot = _root.Q<VisualElement>("inventroy_contain-box");
        ItemManager.Instance.SetInventoryOwner(this);
        ItemManager.Instance.SetInventoryOwner(_doc);
        _tabbarContainDictionary = new Dictionary<TabbarType, Button>();
    }


    private void OnEnable()
    {
        _root.Q<Button>("exit-btn").RegisterCallback<ClickEvent>(HandleBtnclickEvent);
        PageInit();
        TabbarInit();
        ItemSlotInit();
        EquipmentSlotInit();
        EquipmentSpriteInit();
        //Relicinit();
        StatLabelInfo();
        GetItemData();
        GetEquipmentData();
        foreach (EntityStatEnum item in Enum.GetValues(typeof(EntityStatEnum)))
        {
            PlayerManager.Instance.Player.Stat.onStatModified[item] += HandleStatInfoSet;
        }

    }

    private void GetEquipmentData()
    {
        Dictionary<EquipmentType, ItemSO> equipmentDic = _inventorySO.GetEquipmentItem();

        if (equipmentDic == null) return;

        foreach (EquipmentType item in Enum.GetValues(typeof(EquipmentType)))
        {
            if (equipmentDic.ContainsKey(item) == false)
                continue;
            if (equipmentDic[item] == null)
                continue;

            CreateEquipment(equipmentDic[item]);
        }

    }

    private void GetItemData()
    {
        ItemSO[] itemSOArr = _inventorySO.GetInventoryItem();

        if (itemSOArr == null) return;

        for (int i = 0; i < _itemSlotSize; ++i)
        {
            if (itemSOArr[i] == null)
                continue;

            CreateItem(itemSOArr[i]);
        }
    }

    public void HandleBtnclickEvent(ClickEvent evt)
    {
        _inventoryRoot.ToggleInClassList("on");
    }


    private void StatLabelInfo()
    {
        _statLabelDictionary = new Dictionary<EntityStatEnum, Label>();
        foreach (EntityStatEnum value in Enum.GetValues(typeof(EntityStatEnum)))
        {
            if (value == EntityStatEnum.AttackP || value == EntityStatEnum.DefenseP)
                continue;

            _statLabelDictionary.Add(value, _root.Q<VisualElement>($"info_{value.ToString().ToLower()}_stat_contain-box").Q<Label>("info_stat_value-label"));
            _statLabelDictionary[value].text = $"{value}";
        }
        HandleStatInfoSet(PlayerManager.Instance.Player.Stat);
    }

    private void HandleStatInfoSet(EntityStat stat)
    {
        foreach (EntityStatEnum value in Enum.GetValues(typeof(EntityStatEnum)))
        {
            if (value == EntityStatEnum.AttackP || value == EntityStatEnum.DefenseP)
                continue;
            _statLabelDictionary[value].text = $"{stat.statDictionary[value].GetModifiedValue()}";
        }
    }
    //private void Relicinit()
    //{
    //    _relicslotDictionary = new Dictionary<int, RelicSlot>();
    //    for (int i = 1; i <= 3; ++i)
    //    {
    //        var slot = _root.Q<VisualElement>($"{i}_relic_room_contain-box");
    //        _relicslotDictionary.Add(i, new RelicSlot(slot));
    //        _relicslotDictionary[i].SetItemOwner(new Relic(slot, null));
    //    }
    //}

    public bool InventoryContain()
    {

        for (int i = 0; i < _itemSlotSize; ++i)
        {
            if (_itemSlotDictionary[i].IsItemContain() == false)
            {
                return true;
            }
        }
        return false;
    }
    public bool EquipmentContain()
    {
        foreach (EquipmentType item in Enum.GetValues(typeof(EquipmentType)))
        {
            if (_equipmentSlotDictionary[item].IsItemContain() == false)
                return true;

        }
        return false;
    }

    private void EquipmentSpriteInit()
    {
        _equipmentDefaultSpriteDictionary = new Dictionary<EquipmentType, Sprite>();
        for (int i = 0; i < _equipmentListSO.SpriteSOList.Count; ++i)
        {
            var so = _equipmentListSO.SpriteSOList[i];
            _equipmentDefaultSpriteDictionary.Add(so.EquipmentType, so.DefaultSprite);
        }
    }

    private void EquipmentSlotInit()
    {
        _equipmentSlotDictionary = new Dictionary<EquipmentType, EquipmentSlot>();

        var root = _root.Q<VisualElement>("equipment_contain-box");
        foreach (EquipmentType item in Enum.GetValues(typeof(EquipmentType)))
        {
            var slot = root.Q<VisualElement>($"equpment_{item.ToString().ToLower()}_contain-box");
            _equipmentSlotDictionary.Add(item, new EquipmentSlot(slot));
            _equipmentSlotDictionary[item].SetItemOwner(new Equipment(slot, null));
        }
    }

    private void PageInit()
    {
        _pageContainDictionary = new Dictionary<TabbarType, VisualElement>();
        foreach (TabbarType type in Enum.GetValues(typeof(TabbarType)))
        {
            _pageContainDictionary.Add(type, _root.Q<VisualElement>($"{type.ToString().ToLower()}_contain-box"));
        }
        _pageContainDictionary[TabbarType.Equipment].RemoveFromClassList("on");

    }

    private void ItemSlotInit()
    {
        _itemSlotDictionary = new Dictionary<int, ItemSlot>();
        for (int i = 0; i < _itemSlotSize; ++i)
        {
            var root = _root.Q<VisualElement>($"{i}_item_room_contain-box");
            var itemSolot = new ItemSlot(root);

            _itemSlotDictionary.Add(i, itemSolot);
            var templet = _spriteIcon.Instantiate().Q<VisualElement>();
            templet.style.flexGrow = 1;


            _itemSlotDictionary[i].SetItemOwner(new InventoryItem(root, null));
            _itemSlotDictionary[i].CurrentItem.Root.Add(templet);
        }
    }

    private void TabbarInit()
    {
        foreach (TabbarType type in Enum.GetValues(typeof(TabbarType)))
        {
            _tabbarContainDictionary.Add(type, _root.Q<Button>($"tabbar_{type.ToString().ToLower()}-btn"));
            _tabbarContainDictionary[type].AddToClassList("on");

        }

        foreach (TabbarType type1 in Enum.GetValues(typeof(TabbarType)))
        {
            _tabbarContainDictionary[type1].RegisterCallback<ClickEvent>(evt =>
            {
                foreach (TabbarType type2 in Enum.GetValues(typeof(TabbarType)))
                {
                    _tabbarContainDictionary[type2].AddToClassList("on");
                    _pageContainDictionary[type2].AddToClassList("on");
                    if (type2 == type1)
                    {
                        _tabbarContainDictionary[type2].RemoveFromClassList("on");
                        _pageContainDictionary[type2].RemoveFromClassList("on");

                    }
                }
            });
        }
        _tabbarContainDictionary[TabbarType.Equipment].RemoveFromClassList("on");
    }
    public void CreateItem(RelicSO item)
    {
        for (int i = 0; i < _itemSlotSize; ++i)
        {
            if (_itemSlotDictionary[i].IsItemContain() == false)
            {
                _itemSlotDictionary[i].ItemTake(item);
                return;
            }
        }
    }
    public void CreateItem(ItemSO item)
    {
        // 이곳에 현제 인벤토리에 있는 아이템 저장해줘야함
        for (int i = 0; i < _itemSlotSize; ++i)
        {
            if (_itemSlotDictionary[i].IsItemContain() == false)
            {
                _itemSlotDictionary[i].ItemTake(item);
                return;
            }
        }
    }

    public void CreateRelic(RelicSO so)
    {
        for (int i = 1; i <= 3; ++i)
        {
            if (_relicslotDictionary[i].IsItemContain() == false)
            {
                _relicslotDictionary[i].ItemTake(so);
                return;
            }
        }
    }

    public void CreateEquipment(ItemSO item)
    {
        foreach (EquipmentType type in Enum.GetValues(typeof(EquipmentType)))
        {
            if ((int)type == (int)item.ItemType)
            {
                EquipmentSlot slot = _equipmentSlotDictionary[type];
                if (slot.CurrentItem.CurrentItemSO != null)
                {
                    Item currentItem = slot.CurrentItem;
                    foreach (EntityStatEnum stat in Enum.GetValues(typeof(EntityStatEnum))) //StatAdd
                    {
                        if (stat == EntityStatEnum.AttackP || stat == EntityStatEnum.DefenseP)
                            continue;
                        PlayerManager.Instance.Player.Stat.RemoveStatModifier(stat, currentItem.CurrentItemSO.ItemStatDictionary[stat]);
                    }
                    CreateItem(currentItem.CurrentItemSO);
                }
                _equipmentSlotDictionary[type].EquipmentInstallation(item);

                foreach (EntityStatEnum stat in Enum.GetValues(typeof(EntityStatEnum))) //StatAdd
                {
                    if (stat == EntityStatEnum.AttackP || stat == EntityStatEnum.DefenseP)
                        continue;
                    PlayerManager.Instance.Player.Stat.AddStatModifier(stat, item.ItemStatDictionary[stat]);
                }

                return;
            }
        }
    }

    public void DestroyEquipment(Item item)
    {
        foreach (EntityStatEnum stat in Enum.GetValues(typeof(EntityStatEnum)))
        {
            if (stat == EntityStatEnum.AttackP || stat == EntityStatEnum.DefenseP)
                continue;
            PlayerManager.Instance.Player.Stat.RemoveStatModifier(stat, item.CurrentItemSO.ItemStatDictionary[stat]);
        }

        ItemType type = item.ItemType;
        item.CurrentItemSO = null;
        item.ItemType = type;

        EquipmentType equipmentType = (EquipmentType)((int)type);
        var spriteBox = item.SpriteBox;
        spriteBox.style.backgroundImage = new StyleBackground(_equipmentDefaultSpriteDictionary[equipmentType]);
        spriteBox.style.unityBackgroundImageTintColor = _noneColor;

        //여따가 플레이어 스텟 빼는거 넣어야함
    }
    public void DestroyRelic(Relic relic)
    {
        relic.CurrentRelicSO = null;
    }


    private void OnDisable()
    {
        Debug.Log(PlayerManager.Instance.Player);
        for (int i = 0; i < _itemSlotSize; i++)
        {
            if (_itemSlotDictionary[i].CurrentItem == null)
                continue;
            if (_itemSlotDictionary[i].CurrentItem.CurrentItemSO == null)
                continue;

            _inventorySO.ItemAdd(_itemSlotDictionary[i].CurrentItem.CurrentItemSO);

        }

        foreach (EquipmentType item in Enum.GetValues(typeof(EquipmentType)))
        {
            if (_equipmentSlotDictionary.ContainsKey(item) == false)
                continue;
            if (_equipmentSlotDictionary[item] == null)
                continue;

            _inventorySO.EquipmentAdd(_equipmentSlotDictionary[item].CurrentItem.CurrentItemSO, item);
        }

        foreach (EntityStatEnum item in Enum.GetValues(typeof(EntityStatEnum)))
        {
            if (item == EntityStatEnum.AttackP || item == EntityStatEnum.DefenseP)
                continue;
            PlayerManager.Instance.Player.Stat.onStatModified[item] -= HandleStatInfoSet;
        }
    }
}
