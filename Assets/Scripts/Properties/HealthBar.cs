using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _value;

    [field: SerializeField] public Health Health { get; private set; }

    private void Awake() => 
        Init();

    private void OnEnable() => 
        Health.Changed += Set;

    private void OnDisable() => 
        Health.Changed -= Set;

    public void Set(int value)
    {
        string convertedValue = Convert.ToString(value);

        _value.text = $"{convertedValue}/{Health.Max}";
        _image.fillAmount = (float)value / 10;
    }

    private void Init() =>
        Set(Health.Current);
}