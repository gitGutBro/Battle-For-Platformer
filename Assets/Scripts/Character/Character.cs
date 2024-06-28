using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] protected Damager Damager { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
}