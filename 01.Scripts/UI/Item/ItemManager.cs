using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ItemManager : MonoSingleton<ItemManager>
{
    [SerializeField] private UIDocument _inventoryDoc;
    [SerializeField] private VisualTreeAsset _spriteIcon;
    [SerializeField] private ItemSO itemso;
    [SerializeField] private ItemSO itemso1;
    [SerializeField] private ItemSO itemso2;
    [SerializeField] private ItemSO itemso3;
    [SerializeField] private RelicSO itemso4;
    private InventoryUI _inventory;
    private ItemCheckBox _itemCheckBoxUI;


    private void Awake()
    {
        ItemManager.Instance.Init();
    }

    public void Init()
    {

    }

    public void SetInventoryOwner(UIDocument doc)
    {
        _inventoryDoc = doc;
    }


    public void DestoryEquipment(Equipment equipment)
    {
        _inventory.DestroyEquipment(equipment);
    }
    public void DestoryRelic(Relic relic)
    {
        _inventory.DestroyRelic(relic);
    }

    public void SetInventoryOwner(InventoryUI ui)
    {
        _inventory = ui;
    }
    public void SetCheckBoxOwner(ItemCheckBox ui)
    {
        _itemCheckBoxUI = ui;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateItem(itemso);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            CreateItem(itemso1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CreateItem(itemso2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CreateItem(itemso3);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            CreateItem(itemso4);
        }
    }
#endif
    public void CreateItem(ItemSO so)
    {
        if (_inventory.InventoryContain())
        {
            //Scucees
            _inventory.CreateItem(so);
            Debug.Log("¿ﬂµÃ≥Î");
        }
        else
        {
            //FullItem
            Debug.Log("æ∆¿Ã≈€¿Ã ≤À¬˜¿Ω");

        }
    }

    public void CreateItem(RelicSO so)
    {
        if (_inventory.InventoryContain())
        {
            _inventory.CreateItem(so);
            Debug.Log("¿ﬂµÃ≥Î");
        }

    }


    public void CreateEquipment(ItemSO so)
    {
        if (_inventory.EquipmentContain())
            _inventory.CreateEquipment(so);
    }

    public void CreateRelic(RelicSO so)
    {
        if (_inventory.InventoryContain())
            _inventory.CreateRelic(so);
    }


    public void ShowCheckBox(Item item, Vector2 pos, Action PositiveEvent, string positivStr)
    {
        _itemCheckBoxUI.ShowItemInfoBox(item, pos, PositiveEvent, positivStr);
    }
    public void ShowCheckRelicBox(Item item, Vector2 pos, Action PositiveEvent, string positivStr)
    {
        _itemCheckBoxUI.ShowRelicInfoBox(item, pos, PositiveEvent, positivStr);
    }
}
