
public class CardGift : Gift
{
    protected CardDataSO _giveSO;

    public override void SetGiftCard<T>(T so, GiftUI ui)
    {
        _giveSO = so as CardDataSO;
        _giftUI = ui;
        _text.SetText(_giveSO.cardName);
        _btn.onClick.AddListener(OnPointerDown);
    }
    public override void OnPointerDown()
    {
        _giftUI.OpenChoiceWindow(_giveSO);
        //_giftUI.CardDeckSO.Add(_giveSO);
    }

}
