using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDictionaryUI : PopUpUI
{
    [SerializeField] private CardDataSOList _listSO;
    [SerializeField] private CardConfigSO _cardConfigSO;
    [SerializeField] private Transform _root;
    [SerializeField] private RectTransform _rTrm;
    [SerializeField] private GameObject _cardPrefab;


    protected override void Init()
    {

        CardContainSizeSet();

        List<CardDataSO> cardDataList = _listSO.list;

        for (int i = 0; i < cardDataList.Count; ++i)
        {
            CreateCard(cardDataList[i]);
        }
    }

    private void CardContainSizeSet()
    {
        Vector2 delta = _rTrm.sizeDelta;
        Debug.Log(_listSO.list.Count / 5);
        delta.y = (_listSO.list.Count / 5) * 300;
        _rTrm.sizeDelta = delta;
    }

    private void CreateCard(CardDataSO so)
    {
        GameObject card = Instantiate(_cardPrefab, _root);

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
}
