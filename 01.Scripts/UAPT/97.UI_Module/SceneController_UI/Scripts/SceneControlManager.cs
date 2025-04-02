using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoSingleton<SceneControlManager>
{
    [SerializeField] private Image _image;
    [SerializeField] private string _deadSceneName;
    private Color _cr;
    private float _fadeCool = 0.5f;
    private AudioSource _audioSource;
    public AudioSource AudioSource => _audioSource;

    private void Start()
    {
        GameEventBus.Subscribe(GameEventBusType.PlayerDefeat, HandlePlayerDead);
    }

    private void HandlePlayerDead()
    {
        _fadeOut(delegate { SceneManager.LoadScene(_deadSceneName); });
    }

    public void SetAudio(AudioSource audio)
    {
        _audioSource = audio;
    }

    /// <summary>
    /// 0 -> 1
    /// </summary>
    /// <param name="action"></param>
    public static void FadeOut(Action action) =>
        Instance._fadeOut(action);

    /// <summary>
    /// 1 -> 0
    /// </summary>
    /// <param name="action"></param>
    public static void FadeIn(Action action) =>
        Instance._fadeIn(action);


    private void _fadeIn(Action action)
    {
        _image.raycastTarget = false;
        StartCoroutine(fadeInCo(action));
    }

    private IEnumerator fadeInCo(Action action)
    {
        _cr = _image.color;
        while (_image.color.a >= 0)
        {
            float f = Time.deltaTime / _fadeCool;
            _cr.a -= f;
            if (_audioSource)
                _audioSource.volume += f;
            _image.color = _cr;
            yield return null;
        }

        if (FadeCanvas.Instance != null)
            FadeCanvas.Instance.FadeOutEndEvent?.Invoke();
        action?.Invoke();
    }

    private void _fadeOut(Action action)
    {
        _image.raycastTarget = true;
        StopAllCoroutines();
        StartCoroutine(fadeOutCo(action));
    }

    private IEnumerator fadeOutCo(Action action)
    {
        _cr = _image.color;
        while (_image.color.a <= 1)
        {
            float f = Time.deltaTime / _fadeCool;
            _cr.a += f;
            if (_audioSource)
                _audioSource.volume -= f;
            _image.color = _cr;
            yield return null;
        }

        action?.Invoke();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UI_Util.PopupUIList = new System.Collections.Generic.List<PopUpUI>();
        _fadeIn(() => { });
    }

    private void OnDisable()
    {
        GameEventBus.UnSubscribe(GameEventBusType.PlayerDefeat, HandlePlayerDead);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}