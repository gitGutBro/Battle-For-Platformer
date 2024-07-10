using System;
using UnityEngine;

[Serializable]
public class Damager
{
    [SerializeField][Range(1, 10)] private int _damage;
    [SerializeField][Range(0.1f, 1f)] private float _radius;
    [SerializeField] private LayerMask _hitTarget;

    public void Attack(Collider2D hitArea)
    {
        Collider2D[] hitTargets = FindTargets(hitArea);

        foreach (Collider2D target in hitTargets)
            if (target.TryGetComponent(out IDamagable character))
                character.Health.Decrease(_damage);
    }

    private Collider2D[] FindTargets(Collider2D hitArea) =>
        Physics2D.OverlapCircleAll(hitArea.transform.position, _radius, _hitTarget);
}