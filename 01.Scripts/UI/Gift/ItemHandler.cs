using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    public void SetItem(ItemSO so)
    {
        _image.sprite = so.ItemSprite;
    }
}
