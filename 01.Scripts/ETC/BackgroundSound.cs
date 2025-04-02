using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        SceneControlManager.Instance.SetAudio(_audio);
    }

}
