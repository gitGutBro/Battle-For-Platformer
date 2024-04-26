using System;
using UnityEngine;

[Serializable]
public class Purse
{
    [SerializeField] private int _coins;

    public void IncreaseCoins(int countCoins) =>
        _coins += (countCoins > 0) ? countCoins : 0;

    public void DecreaseCoins(int countCoins)
        => _coins -= Mathf.Clamp(countCoins, 0, _coins);
}