using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class DacksettingUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private VisualTreeAsset _treeAsset;
    [SerializeField] private CardConfigSO cardConfigSO;
    private List<VisualElement> _cardListViewList;
    private UIDocument _doc;
    private VisualElement _deckContain;
    private ScrollView _cardScrollview;
    private Button _exitBtn;
    private CardDeckSO _deckSO;
    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        _cardListViewList = new List<VisualElement>();
    }

    private void OnEnable()
    {
        VisualElement root = _doc.rootVisualElement;

        _deckContain = root.Q<VisualElement>("card_contain-box");
        _exitBtn = root.Q<Button>("exit-btn");
        _cardScrollview = root.Q<ScrollView>("card_contain-scrollview");

        _exitBtn.RegisterCallback<ClickEvent>(HandleDackBtn);

        _deckSO = _player.Deck;
        _deckSO.OnDeckChanged += HandleDeckChangedEvent;
        _deckSO.OnHandChanged += HandleDeckChangedEvent;

        HandleDeckChangedEvent(_deckSO.deck);
        HandleDeckChangedEvent(_deckSO.hand);
    }

    private void HandleDeckChangedEvent(IList<CardDataSO> value)
    {
        for (int i = 0; i < value.Count; ++i)
        {
            CreateCard(value[i]);
        }
    }

    public void HandleDeck()
    {
        _cardScrollview.Clear();
        for (int i = 0; i < _deckSO.deck.Count; i++)
        {
            CreateCard(_deckSO.deck[i]);
        }
    }

    private void CreateCard(CardDataSO so)//테이블에 생길 카드
    {
        var instant = _treeAsset.Instantiate().Q<Button>();

        instant.Q<Label>("card_name-label").text = so.cardName;

        string cardTypeStr = cardConfigSO.cardTypeNameDictionary[so.cardType].ToString();
        instant.Q<Label>("card_stat-label").text = cardTypeStr;

        instant.Q<Label>("card_info-label").text = so.description;

        _cardScrollview.Add(instant);

    }

    public void HandleDackBtn(ClickEvent evt)
    {
        _deckContain.ToggleInClassList("on");
    }

    private void OnDisable()
    {
        _exitBtn.UnregisterCallback<ClickEvent>(HandleDackBtn);
    }

    private CardDeckSO GetDeck()
    {
        return _player.Deck;
    }
}
