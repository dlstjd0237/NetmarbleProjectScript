using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionUI : PopUpUI
{
    [SerializeField] private Button _titleSceneBtn;
    [SerializeField] private string _loadSceneName;


    protected override void Awake()
    {
        base.Awake();

        if(_titleSceneBtn != null)
        _titleSceneBtn.onClick.AddListener(HandleTitleChangeEvent);
    }

    private void HandleTitleChangeEvent()
    {
        SceneControlManager.FadeOut(() => SceneManager.LoadScene(_loadSceneName));
    }
}
