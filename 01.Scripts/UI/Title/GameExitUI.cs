using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameExitUI : PopUpUI
{
    [SerializeField] private Button _exitBtn;

    protected override void Awake()
    {
        base.Awake();
        _exitBtn.onClick.AddListener(delegate { SceneControlManager.FadeOut(() => Application.Quit()); });
    }
}
