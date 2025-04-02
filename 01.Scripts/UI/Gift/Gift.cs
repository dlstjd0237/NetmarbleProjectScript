using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gift : MonoBehaviour
{
    protected TextMeshProUGUI _text;
    protected GiftUI _giftUI;
    protected Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
        _text = transform.GetComponentInChildren<TextMeshProUGUI>();
    }


    /// <summary>
    /// CardUI 생성시 호출하여 카드 이미지를 그려줌
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="so"></param>
    public abstract void SetGiftCard<T>(T so, GiftUI ui) where T : ScriptableObject;
    public abstract void OnPointerDown();
}
