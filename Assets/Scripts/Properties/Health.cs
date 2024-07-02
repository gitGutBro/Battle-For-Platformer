using System;
using UnityEngine;

[Serializable]
public class Health
{
    public const int Max = 10;
    private const int Min = 0;

    [SerializeField][Range(Min, Max)] private int _current;

    public Health() =>
        _current = Max;

    public event Action Died;
    public event Action<int> Changed;

    public int Current => _current;

    public void Increase(int heal) => 
        _current = Mathf.Clamp
        (_current + heal, 
        Min, Max);

    public void Decrease(int damage)
    {
        _current = Mathf.Clamp
        (_current - damage,
        Min, Max);

        Changed?.Invoke(_current);

        if (_current <= Min)
            Died.Invoke();
    }
}