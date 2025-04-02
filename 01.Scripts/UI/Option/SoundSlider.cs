using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundSlider : MonoBehaviour
{
    [SerializeField] private SoundType _type;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(HandleChangeVlaueEvent);
    }
    private void Start()
    {
        _slider.value = SoundManager.Instance.GetVolume(_type);
        
    }
    private void HandleChangeVlaueEvent(float value)
    {
        switch (_type)
        {
            case SoundType.BGM:
                SoundManager.Instance.VolumeSetMusic(value);
                break;
            case SoundType.SFX:
                SoundManager.Instance.VolumeSetSFX(value);
                break;
            case SoundType.Master:
                SoundManager.Instance.VolumeSetMaster(value);
                break;
        }
    }
}
