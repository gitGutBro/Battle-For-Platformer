using UnityEngine;

public class Enemy : Character
{
    [Header("Target Settings")]
    [SerializeField] private LayerMask _target;
    [SerializeField] private float _detectionDistance;
    [Space]
    [SerializeField] private EnemyMover _mover;
}