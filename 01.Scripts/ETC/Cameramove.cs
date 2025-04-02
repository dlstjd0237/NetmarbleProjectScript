using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Cameramove : MonoBehaviour
{
    [SerializeField] private float _duration = 3.0f;
    [SerializeField] private float _endValue = 13.0f;


    private void Awake()
    {
        transform.DOMoveY(_endValue, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
