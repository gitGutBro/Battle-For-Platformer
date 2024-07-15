using UnityEngine;

public class InputService
{
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode Jump = KeyCode.W;
    private const KeyCode Attack = KeyCode.Space;
    private const KeyCode Skill = KeyCode.R;

    public bool IsJumped { get; private set; }
    public float AxisHorizontal { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsSkillActive { get; private set; }

    public void Update()
    {
        AxisHorizontal = Input.GetAxis(Horizontal);
        IsJumped = Input.GetKeyDown(Jump);
        IsAttacking = Input.GetKey(Attack);
        IsSkillActive = Input.GetKey(Skill);
    }
}