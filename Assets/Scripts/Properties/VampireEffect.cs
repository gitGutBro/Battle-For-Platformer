using UnityEngine;

public class VampireEffect
{
    public const int VampireDamage = 10;
    private const float ActivateDelay = 0.5f;
    
    public float CurrentTime { get; private set; }

    public void AttackByTimer(IDamagable damagable)
    {
        if (CurrentTime < ActivateDelay)
            return;

        CurrentTime = 0;

        damagable.Health.Decrease(VampireDamage);
    }

    public void StartTimer() => 
        CurrentTime += Time.deltaTime;

    public void ResetTimer() => 
        CurrentTime = 0;
}
