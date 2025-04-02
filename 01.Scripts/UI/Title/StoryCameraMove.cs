using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StoryCameraMove : MonoBehaviour
{
    [SerializeField] private List<Vector3> _camMovePointPage1;
    [SerializeField] private List<Image> _imageCutList;
    [SerializeField] private float _duration = 2f;
    [SerializeField] private string _titleSceneName;
    [SerializeField] private Button _btn;
    [SerializeField] private AudioClip _soundClip;
    private int _index = 0;
    private bool _isPageOne = false;
    private void Awake()
    {
        _btn.onClick.AddListener(HandleNext);
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < _imageCutList.Count; ++i)
        {
            Color WhtieColor = Color.white;
            WhtieColor.a = 0;

            _imageCutList[i].color = WhtieColor;
        }
    }

    private void HandleNext()
    {
        if (_index == 5 && _isPageOne == false)
        {
            for (int i = 0; i < 5; ++i)
            {
                _imageCutList[i].DOFade(0, 0.5f);
            }
            _isPageOne = true;

        }
        else if (_index < 10)
        {
            SoundManager.PlaySound(_soundClip, SoundType.SFX);
            _imageCutList[_index].DOFade(1, 0.5f);
            transform.DOMove(_camMovePointPage1[_index++], _duration);
        }
        else
        {
            Init();
            SceneControlManager.FadeOut(() =>
            {
                SceneManager.LoadScene(_titleSceneName);
            });
        }


    }
}
