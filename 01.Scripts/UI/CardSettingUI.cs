using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;

public class CardSettingUI : MonoBehaviour
{
    [SerializeField] private CardDeckSO _deckSO;
    [SerializeField] private CardConfigSO _cardConfigSO;
    [SerializeField] private Transform _root;
    [SerializeField] private RectTransform _rTrm;
    [SerializeField] private CardHandler _cardPrefab;

    private float _duration = 0;
    [SerializeField] private Button _windowCloseBtn, _blindWindowCloseBtn;
    [SerializeField] private CanvasGroup _blindCG;
    [SerializeField] private CardHandler _currentChoiceCardHandler;
    private CanvasGroup _cG;

    private void Awake()
    {
        _cG = GetComponent<CanvasGroup>();
        CardContainSizeSet();
        _windowCloseBtn.onClick.AddListener(HandleCloseWindow);
        _blindWindowCloseBtn.onClick.AddListener(HandleCloseBlindWindow);
        CardContainInit();
    }

    private void HandleCloseBlindWindow()
    {
        ActiveCanvasGroup(_blindCG, false);
    }

    private void ActiveCanvasGroup(CanvasGroup canvasGroup, bool value)
    {
        int fadeValue = value ? 1 : 0;
        canvasGroup.DOFade(fadeValue, _duration);
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
    }

    private void CardContainInit()
    {
        for (int i = 0; i < _deckSO.deck.Count; ++i)
        {
            CreateCard(_deckSO.deck[i]);
        }
        for (int i = 0; i < _deckSO.hand.Count; ++i)
        {
            CreateCard(_deckSO.hand[i]);
        }
    }
    private void CardContainSizeSet()
    {
        Vector2 delta = _rTrm.sizeDelta;

        delta.y = (_deckSO.deck.Count / 5) * 280;
        Debug.Log(_deckSO.hand.Count / 5);
        if (_deckSO.hand.Count / 5 == 0 && _deckSO.hand.Count > 0)
            delta.y += 280;

        delta.y += (_deckSO.hand.Count / 5) * 280;

        if (_deckSO.deck.Count / 5 == 0 && _deckSO.deck.Count > 0)
            delta.y += 280;

        _rTrm.sizeDelta = delta;
    }
    private void HandleCloseWindow()
    {
        WindowActive(false);
    }

    public void HandleOpenWindow()
    {
        WindowActive(true);
    }

    public void WindowActive(bool value)
    {
        int endValue = value ? 1 : 0;
        _cG.interactable = value;
        _cG.blocksRaycasts = value;
        _cG.DOFade(endValue, _duration);
    }
    private void SetCardInfo(CardHandler card, CardDataSO so)
    {
        Transform descriptionBGTrm = card.transform.Find("DescriptionBG").transform;

        Transform iconBGTrm = card.transform.Find("IconBG").transform;
        Transform MaskTrm = iconBGTrm.transform.Find("Mask").transform;
        Transform TypeTrm = iconBGTrm.transform.Find("TypeBG").transform;

        Transform nameMarkTrm = card.transform.Find("NameMark").transform;

        Transform costTrm = card.transform.Find("Cost").transform;

        card.GetComponent<Image>().sprite = so.bgSprite;
        descriptionBGTrm.GetComponent<Image>().sprite = so.descriptionBGSprite;
        descriptionBGTrm.Find("Description").GetComponent<TextMeshProUGUI>().SetText(so.description);

        string cardTypeStr = _cardConfigSO.cardTypeNameDictionary[so.cardType].ToString();
        iconBGTrm.GetComponent<Image>().sprite = so.iconMaskSprite;

        MaskTrm.GetComponent<Image>().sprite = so.iconMaskSprite;
        MaskTrm.Find("Icon").GetComponent<Image>().sprite = so.cardIconSprite;

        TypeTrm.GetComponent<Image>().sprite = so.typeBGSprite;
        TypeTrm.Find("Type").GetComponent<TextMeshProUGUI>().SetText(cardTypeStr);

        nameMarkTrm.GetComponent<Image>().sprite = so.nameMarkSprite;
        nameMarkTrm.Find("Name").GetComponent<TextMeshProUGUI>().SetText(so.cardName);

        costTrm.GetComponent<Image>().sprite = so.costSprite;
        costTrm.Find("Text").GetComponent<TextMeshProUGUI>().SetText(so.cost.ToString());
    }

    private void CreateCard(CardDataSO so)
    {
        CardHandler card = Instantiate(_cardPrefab, _root);

        SetCardInfo(card, so);

        card.DataSO = so;
        card.ClickEvent += ClickHandleEvent;
    }

    private void ClickHandleEvent(CardDataSO so)
    {
        ActiveCanvasGroup(_blindCG, true);
        SetCardInfo(_currentChoiceCardHandler, so);
    }
}
