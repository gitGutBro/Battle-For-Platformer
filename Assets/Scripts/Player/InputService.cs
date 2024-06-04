using UnityEngine;

public class InputService
{
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode JumpKey = KeyCode.W;

    public bool IsJumped { get; private set; }
    public float AxisHorizontal { get; private set; }

    public void Update()
    {
        IsJumped = Input.GetKeyDown(JumpKey);
        AxisHorizontal = Input.GetAxis(Horizontal);
    }
}