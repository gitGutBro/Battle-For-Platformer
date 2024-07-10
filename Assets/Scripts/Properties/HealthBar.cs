using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _value;

    [SerializeField] private MonoBehaviour _damagableObject;
    
    private IDamagable _damagable;

    private void OnValidate()
    {
        if (_damagableObject is IDamagable damagable)
        {
            _damagable = damagable;
        }
        else
        {
            _damagable = null;
            _damagableObject = null;
            return;
        }

        if (_damagable.Health.Current > _damagable.Health.Max)
            _damagable.Health.Decrease(_damagable.Health.Current - _damagable.Health.Max);
    }

    private void OnEnable() => 
        _damagable.Health.Changed += OnHealthChanged;

    private void Start() => 
        Init();

    private void OnDisable() => 
        _damagable.Health.Changed -= OnHealthChanged;

    private void OnHealthChanged(int health)
    {
        _value.text = $"{health:F0}/{_damagable.Health.Max}";

        if (_damagable.Health.Max == 0)
        {
            Debug.LogError($"Value _damagable.Health.Max is zero! {GetType()}");
            return;
        }

        _image.fillAmount = (float)health / _damagable.Health.Max;
    }

    private void Init() =>
        OnHealthChanged(_damagable.Health.Current);
}