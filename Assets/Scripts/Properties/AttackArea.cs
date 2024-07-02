using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackArea : MonoBehaviour
{
    public Collider2D Area { get; private set; }

    private void Awake() => 
        Area = GetComponent<Collider2D>();

    public void Off() => 
        enabled = false;
}