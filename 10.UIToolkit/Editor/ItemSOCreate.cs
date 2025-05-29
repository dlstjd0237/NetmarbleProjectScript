using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public class ItemSOCreate : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    private VisualElement _spriteContainBox;
    private ObjectField _spriteField;
    private TextField _nameTextField;
    private Button _createBtn;
    private EnumField _enumField, _elementEnumField;
    private Dictionary<EntityStatEnum, IntegerField> _statFieldDictionary;

    [MenuItem("Tools/Baek/ItemSOCreate")]
    public static void ShowExample()
    {
        ItemSOCreate wnd = GetWindow<ItemSOCreate>();
        wnd.titleContent = new GUIContent("ItemSOCreate");

        wnd.minSize = new Vector2(350, 400);
        wnd.maxSize = new Vector2(350, 400);
    }


    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        labelFromUXML.style.flexGrow = 1;
        root.Add(labelFromUXML);

        // 각 UI 요소들 초기화
        _nameTextField = labelFromUXML.Q<TextField>("item_name-textField");
        _spriteContainBox = labelFromUXML.Q<VisualElement>("sprite_icon_contain-box");
        _spriteField = labelFromUXML.Q<ObjectField>("sprite-field");
        
         // 스프라이트 변경 시 반영
        _spriteField.RegisterCallback<ChangeEvent<Object>>(evt => _spriteContainBox.style.backgroundImage = new StyleBackground(evt.newValue as Sprite));
        
        _createBtn = labelFromUXML.Q<Button>("create-btn");
        _enumField = labelFromUXML.Q<EnumField>("item_type-enumfield");
        _elementEnumField = labelFromUXML.Q<EnumField>("item_element_type-enumfield");

        // 스탯 필드 초기화
        _statFieldDictionary = new Dictionary<EntityStatEnum, IntegerField>();
        foreach (EntityStatEnum stat in Enum.GetValues(typeof(EntityStatEnum)))
        {
            if (stat == EntityStatEnum.AttackP || stat == EntityStatEnum.DefenseP)
                continue;

            Debug.Log(labelFromUXML.Q<IntegerField>($"stat_{stat.ToString().ToLower()}-field"));
            _statFieldDictionary.Add(stat, labelFromUXML.Q<IntegerField>($"stat_{stat.ToString().ToLower()}-field"));
        }


        _createBtn.RegisterCallback<ClickEvent>(CreateSO);
    }
    private void CreateSO(ClickEvent evt)
    {
        if (_nameTextField.value == null || _spriteField.value == null || _enumField.value == null)
            return;


        string SOname = _nameTextField.value;
        string filename = $"Assets/ItemSO/{_enumField.value}SO/{SOname}SO.asset";
        ItemSO asset = AssetDatabase.LoadAssetAtPath<ItemSO>(filename);

        if (asset != null)
        {

            asset.ItemName = _nameTextField.value;
            asset.ItemSprite = _spriteField.value as Sprite;
            asset.ItemType = (ItemType)_enumField.value;
            asset.ElementType = (ItemElementType)_elementEnumField.value;
            StatSet(asset);
            EditorUtility.SetDirty(asset);//디스크에 저장
            AssetDatabase.SaveAssets();//유니티 메모리에 저장
        }
        else
        {
            asset = ScriptableObject.CreateInstance<ItemSO>();

            asset.ItemName = _nameTextField.value;
            asset.ItemSprite = _spriteField.value as Sprite;
            asset.ItemType = (ItemType)_enumField.value;
            asset.ElementType = (ItemElementType)_elementEnumField.value;
            StatSet(asset);
            string assetPath = AssetDatabase.GenerateUniqueAssetPath($"Assets/ItemSO/{_enumField.value}SO/{SOname}.asset");
            AssetDatabase.CreateAsset(asset, filename);
            AssetDatabase.Refresh();
        }

    }

    private void StatSet(ItemSO asset)
    {
        foreach (EntityStatEnum stat in Enum.GetValues(typeof(EntityStatEnum)))
        {
            if (stat == EntityStatEnum.AttackP || stat == EntityStatEnum.DefenseP)
                continue;
            Type t = typeof(ItemSO);
            FieldInfo property = t.GetField(stat.ToString());
            Debug.Log(property);

            if (property != null)
            {
                int value = _statFieldDictionary[stat].value;
                property.SetValue(asset, value);
            }
        }

    }
}
