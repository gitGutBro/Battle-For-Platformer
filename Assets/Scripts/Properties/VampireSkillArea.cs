using System;
using UnityEngine;

public class VampireSkillArea : MonoBehaviour
{
    private const float ActivityDelay = 6f;

    private float _currentDelay;
    private VampireEffect _vampireEffect;

    public event Action<int> HealthTook;

    public bool CanActivate => _currentDelay == 0;

    private void Awake() => 
        _vampireEffect = new();

    private void Start() => 
        gameObject.SetActive(false);

    private void Update()
    {
        _vampireEffect.StartTimer();

        _currentDelay += Time.deltaTime;

        if (_currentDelay >= ActivityDelay)
        {
            _currentDelay = 0;
            _vampireEffect.ResetTimer();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy target))
        {
            _vampireEffect.AttackByTimer(target);

            if (_vampireEffect.CurrentTime == 0)
                HealthTook.Invoke(VampireEffect.VampireDamage);
        }
    }
}