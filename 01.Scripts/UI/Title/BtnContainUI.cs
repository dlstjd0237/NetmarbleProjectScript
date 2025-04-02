using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
public class BtnContainUI : MonoBehaviour
{
    [SerializeField] private List<ToolkitButton> _topButtonList;
    [SerializeField] private AudioClip _clickClip;
    private void Awake()
    {
        for (int i = 0; i < _topButtonList.Count; ++i)
        {
            _topButtonList[i].Init();
            _topButtonList[i].ClickEvent.AddListener(delegate { SoundManager.PlaySound(_clickClip, SoundType.SFX); });

        }
    }
}

