using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class PopUpUI : MonoBehaviour
{
    [Header("WindowSetting")]
    protected float _duration = 0;
    [SerializeField] protected Button _windowExitBtn, _windowBtn;
    [Header("WindowSoundSetting")]
    [SerializeField] protected AudioClip _windowBtnCliekClip = default;
    protected CanvasGroup _cG;

    protected virtual void Awake()
    {
        _cG = GetComponent<CanvasGroup>();
        UI_Util.Subscription(this);
        if (_windowBtn != null)
            _windowBtn.onClick.AddListener(HandleWindowOpenEvent);
        if (_windowExitBtn != null)
            _windowExitBtn.onClick.AddListener(HandleWindowCloseEvent);
        Init();
    }

    protected virtual void HandleWindowCloseEvent()
    {
        ShowWindow(false);
    }

    protected virtual void HandleWindowOpenEvent()
    {
        ShowWindow(true);
    }

    public void ShowWindow(bool value)
    {
        if (_windowBtnCliekClip != null)
            SoundManager.PlaySound(_windowBtnCliekClip, SoundType.SFX);
        if (value)
        {
            UI_Util.AllHidePopupUI(this);
            _cG.interactable = value;
            _cG.blocksRaycasts = value;
            _cG.DOFade(1, _duration);
        }
        else
        {
            _cG.interactable = value;
            _cG.blocksRaycasts = value;
            _cG.DOFade(0, _duration);
        }
    }

    protected virtual void Init()
    {

    }
}
