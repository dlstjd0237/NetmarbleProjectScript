using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StoryPopupUI : PopUpUI
{

    [SerializeField] private string storySceneName;
    [SerializeField] private Button _checkBtn;

    protected override void Awake()
    {
        base.Awake();
        _checkBtn.onClick.AddListener(HandleCheckBtn);
    }

    private void HandleCheckBtn()
    {
        SceneControlManager.FadeOut(delegate { SceneManager.LoadScene(storySceneName); });
    }
}
