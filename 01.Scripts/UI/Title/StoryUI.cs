using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StoryUI : MonoBehaviour
{
    [SerializeField] private string _storySceneName;
    [SerializeField] private Button _storyBtn;

    private void Awake()
    {
        _storyBtn.onClick.AddListener(HandleStoryButton);
    }

    private void HandleStoryButton()
    {
        SceneControlManager.FadeOut(() => SceneManager.LoadScene(_storySceneName));
    }
}
