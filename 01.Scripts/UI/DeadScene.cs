using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DeadScene : MonoBehaviour
{
    [SerializeField]
    private Button _retryBtn, _quitBtn;
    [SerializeField]
    private string _mainMenuName;
    private void Awake()
    {
        _retryBtn.onClick.AddListener(HandleRetry);
        _quitBtn.onClick.AddListener(HandleQuit);
    }

    private void HandleQuit()
    {
        SceneControlManager.FadeOut(delegate { Application.Quit(); });
    }

    private void HandleRetry()
    {
        SceneControlManager.FadeOut(delegate { SceneManager.LoadScene(_mainMenuName); });
    }
}
