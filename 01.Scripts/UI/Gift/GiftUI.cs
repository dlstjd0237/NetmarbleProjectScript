using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GiftUI : MonoBehaviour
{
    private CanvasGroup _cG;
    [SerializeField] private CanvasGroup _choiceCG;
    [SerializeField] private GameObject _giftCard;
    [SerializeField] private Button _dropBtn, _getBtn;
    [SerializeField] private Transform Root;
    [SerializeField] private Button _exitBtn;
    [SerializeField] private CardDeckSO _cardDeckSO;
    [SerializeField] private CardHandler _cardHandler;
    [SerializeField] private ItemHandler _itemHandler;
    [SerializeField] private CardDataSOList _cardList;
    [SerializeField] private ItemContainSO _itemList;

    public CardDeckSO CardDeckSO => _cardDeckSO;
    [SerializeField] private string _slelectSceneName;
    private Dictionary<ScriptableObject, GameObject> _giftObjDic;
    private void Awake()
    {
        _cG = GetComponent<CanvasGroup>();
        _exitBtn.onClick.AddListener(CloseGiftWindow);
        GameEventBus.Subscribe(GameEventBusType.EnemyDefeat, handleOpen);
    }
    private void OnDisable()
    {
        GameEventBus.UnSubscribe(GameEventBusType.EnemyDefeat, handleOpen);

    }

    private void handleOpen()
    {
        OpenGiftWindow(_cardList.list, _itemList.itemSOList);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
            GameEventBus.Publish(GameEventBusType.EnemyDefeat);
    }

    public void OpenGiftWindow(List<CardDataSO> giftCardList, List<ItemSO> giftItemList)
    {
        _giftObjDic = new Dictionary<ScriptableObject, GameObject>();


        if (giftCardList != null)
        {
            for (int i = 0; i < 2; ++i)
            {
                GameObject card = Instantiate(_giftCard, Root);
                CardGift cardGift = card.AddComponent<CardGift>();
                int randNum = Random.Range(0, giftCardList.Count);
                cardGift.SetGiftCard(giftCardList[randNum], this);
                _giftObjDic.Add(giftCardList[randNum], card);
            }
        }



        if (giftItemList != null)
        {
            for (int i = 0; i <2; ++i)
            {
                GameObject equipemnt = Instantiate(_giftCard, Root);
                EquipmentGift equipGift = equipemnt.AddComponent<EquipmentGift>();
                int randomNum = Random.Range(0, giftItemList.Count);
                equipGift.SetGiftCard(giftItemList[randomNum], this);
                _giftObjDic.Add(giftItemList[randomNum], equipemnt);
            }
        }

        UI_Util.WindowActive(_cG, true, 0.3f);
    }

    public void OpenChoiceWindow(CardDataSO so)
    {
        UI_Util.WindowActive(_choiceCG, true, 0);
        _cardHandler.gameObject.SetActive(true);
        _cardHandler.CreateCard(so);
        _dropBtn.onClick.AddListener(delegate { CloseChoiceWindow(so); });
        _getBtn.onClick.AddListener(delegate { GetSoBtn(so); });
    }

    public void OpenChoiceWindow(ItemSO so)
    {
        UI_Util.WindowActive(_choiceCG, true, 0);
        _itemHandler.gameObject.SetActive(true);
        _itemHandler.SetItem(so);

        _dropBtn.onClick.AddListener(delegate { CloseChoiceWindow(so); });
        _getBtn.onClick.AddListener(delegate { GetSoBtn(so); });
    }


    public void GetSoBtn(ItemSO so)
    {
        ItemManager.Instance.CreateItem(so);
        CloseChoiceWindow(so);
    }
    public void GetSoBtn(CardDataSO so)
    {
        _cardDeckSO.Add(so);
        CloseChoiceWindow(so);
    }

    private void DestroyGiftObject(ScriptableObject so)
    {
        Destroy(_giftObjDic[so].gameObject);
        _giftObjDic.Remove(so);
    }
    public void CloseChoiceWindow(ScriptableObject so)
    {
        _itemHandler.gameObject.SetActive(false);
        _cardHandler.gameObject.SetActive(false);
        UI_Util.WindowActive(_choiceCG, false, 0);
        DestroyGiftObject(so);


        _dropBtn.onClick.RemoveAllListeners();
        _getBtn.onClick.RemoveAllListeners();
    }

    //private void HandlerClickEvent()
    //{ 
    //    Debug.Log("Å¬¸¯‰çÀ½");
    //    _cardDeckSO.Add(so);
    //    Destroy(cardObj);
    //}

    public void CloseGiftWindow()
    {
        foreach (var item in _giftObjDic)
        {
            Destroy(_giftObjDic[item.Key]);
        }
        UI_Util.WindowActive(_cG, false, 0.3f);
        SceneControlManager.FadeOut(() => SceneManager.LoadScene(_slelectSceneName));
    }


}
