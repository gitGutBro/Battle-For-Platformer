using System.Threading.Tasks;
using UnityEngine;

[SelectionBase]
public abstract class Character : MonoBehaviour
{
    private const int DelayMultiplier = 1000;

    [SerializeField, Min(0)] private float _offDelay;
    [Header("Stats")]
    [SerializeField] private Health _health;

    private string _name;

    public Health GetHealth => _health;
    public bool IsDead => _health.CurrentValue == 0;
    protected bool HasMaxHealth => _health.IsMaxValue;

    private void Awake()
        => Init();

    public void Heal(int healValue)
    {
        if (_health.TryIncrease(healValue))
        {
            print($"{_name} take heal {healValue}");
        }
    }

    public void TakeDamage(int damage)
    {
        if (_health.TryDecrease(damage) == false || IsDead == true)
        {
            return;
        }

        print($"{_name} take {damage} damage");

        if (_health.CurrentValue == 0)
        {
            print($"{_name} is dead");
        }
    }

    protected virtual void OnInit() { }

    protected void OffThisObject()
    {
        int timeInMiliseconds = (int)_offDelay * DelayMultiplier;
        Task.Delay(timeInMiliseconds);
        enabled = false;
    }

    private void Init()
    {
        _name = gameObject.name;

        OnInit();
    }
}