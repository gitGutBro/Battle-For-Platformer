using System;

public class EnemyAttackArea : AttackArea
{
    public event Action CharacterNotAttacking;

    private void OnTriggerExit2D() =>
        CharacterNotAttacking.Invoke();
}