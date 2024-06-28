using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackArea : MonoBehaviour
{
    public event Action CharacterAttacking;

    public Collider2D Area { get; private set; }

    private void Awake() => 
        Area = GetComponent<Collider2D>();

    private void OnTriggerStay2D() => 
        CharacterAttacking.Invoke();
}