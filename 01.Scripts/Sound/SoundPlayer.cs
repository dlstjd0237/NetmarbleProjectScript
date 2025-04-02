using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource _aS;

    private void Awake()
    {
        _aS = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        _aS.clip = clip;
        _aS.Play();
        Invoke(nameof(Active), 5);
    }
    private void Active()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        PoolManager.ReturnToPool(gameObject);
    }
}
