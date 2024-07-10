using System;
using UnityEngine;

[Serializable]
public class Health
{
    private const int Min = 0;

    [SerializeField][Min(Min)] private int _current;

    [field: SerializeField] public int Max { get; private set; }

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
            Died?.Invoke();
    }
}