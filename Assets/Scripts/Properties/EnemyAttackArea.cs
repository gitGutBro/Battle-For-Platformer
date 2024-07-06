using System;

public class EnemyAttackArea : AttackArea
{
    public event Action CharacterAttacking;
    public event Action CharacterNotAttacking;

    private void OnTriggerStay2D() => 
        CharacterAttacking.Invoke();

    private void OnTriggerExit2D() =>
        CharacterNotAttacking.Invoke();
}