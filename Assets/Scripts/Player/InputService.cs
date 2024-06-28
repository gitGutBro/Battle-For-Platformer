using UnityEngine;

public class InputService
{
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode JumpKey = KeyCode.W;
    private const KeyCode Attack = KeyCode.Space;

    public bool IsJumped { get; private set; }
    public float AxisHorizontal { get; private set; }
    public bool IsAttacking { get; private set; }

    public void Update()
    {
        IsJumped = Input.GetKeyDown(JumpKey);
        AxisHorizontal = Input.GetAxis(Horizontal);
        IsAttacking = Input.GetKey(Attack);
    }
}