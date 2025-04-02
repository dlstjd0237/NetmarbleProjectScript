using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Ease _ease;
    [SerializeField] private float _duration = 2.5f;
    [SerializeField] private Button _touchBtn;
    //private CanvasGroup _canvasGropup;

    private void Awake()
    {
        //_canvasGropup = GetComponent<CanvasGroup>();
        _touchBtn.onClick.AddListener(HandleTouchEvent);
        if (PlayerManager.Instance.Player)
        {

        }
    }

    private void HandleTouchEvent()
    {
        //_canvasGropup.blocksRaycasts = false;
        //이곳에 화면 전환되는 화면 나와야함
    }

    private void OnEnable()
    {
        _text.DOFade(0, _duration).SetEase(_ease).SetLoops(-1, LoopType.Yoyo);
    }

}
