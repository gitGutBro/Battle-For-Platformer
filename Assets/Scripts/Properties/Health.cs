using System;
using UnityEngine;

[Serializable]
public class Health
{
    private const int MaxValue = 10;
    private const int MinValue = 0;

    private int _currentValue;

    public Health() =>
        _currentValue = MaxValue;

    public void Increase(int healValue) => 
        _currentValue = Mathf.Clamp(_currentValue + healValue, MinValue, MaxValue);

    public void Decrease(int damageValue) => 
        _currentValue = Mathf.Clamp(_currentValue - damageValue, MinValue, MaxValue);
}