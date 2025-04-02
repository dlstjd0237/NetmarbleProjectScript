using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private CardConfigSO _cardConfigSO;
    public event Action<CardDataSO> ClickEvent;
    public CardDataSO DataSO { get; set; }
    public void OnPointerClick(PointerEventData eventData)
    {
        ClickEvent?.Invoke(DataSO);
    }
    public void CreateCard(CardDataSO so)
    {
        Transform descriptionBGTrm = transform.Find("DescriptionBG").transform;

        Transform iconBGTrm = transform.Find("IconBG").transform;
        Transform MaskTrm = iconBGTrm.transform.Find("Mask").transform;
        Transform TypeTrm = iconBGTrm.transform.Find("TypeBG").transform;

        Transform nameMarkTrm = transform.Find("NameMark").transform;

        Transform costTrm = transform.Find("Cost").transform;

        GetComponent<Image>().sprite = so.bgSprite;
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
