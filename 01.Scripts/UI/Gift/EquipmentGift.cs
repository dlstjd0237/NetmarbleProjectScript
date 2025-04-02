public class EquipmentGift : Gift
{
    protected ItemSO _giveSO;

    public override void SetGiftCard<T>(T so, GiftUI ui)
    {
        _giveSO = so as ItemSO;
        _giftUI = ui;
        _text.SetText(_giveSO.ItemName);
        _btn.onClick.AddListener(OnPointerDown);
    }
    public override void OnPointerDown()
    {
        _giftUI.OpenChoiceWindow(_giveSO);
    }
}
