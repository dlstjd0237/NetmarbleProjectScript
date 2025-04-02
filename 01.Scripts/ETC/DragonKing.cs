using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DragonKing : MonoBehaviour
{
    private Health _health;
    private bool _use = false;
    [SerializeField] private string _endingSceneName;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        if (_health.health <= 0 && _use == false)
        {
            _use = true;
            SceneControlManager.FadeOut(() => SceneManager.LoadScene(_endingSceneName));
        }
    }
}
