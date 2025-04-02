using UnityEngine;

[CreateAssetMenu(menuName ="SO/Baek/PoolListSO")]
public class PoolListSO : ScriptableObject
{
    [SerializeField] Pool[] _pools; public Pool[] Pools { get { return _pools; } }
}
