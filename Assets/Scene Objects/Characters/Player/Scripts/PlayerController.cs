using System;
using UnityEngine;

[Serializable]
public class PlayerController : CharacterMover
{
    private const int JumpForceMultiplier = 50;

    [SerializeField, Min(0)] private float _jumpForce;

    [Header("Ground Check")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundTransform;
    [SerializeField] private Vector2 _groundPointSize;

    public bool IsGrounded => WasGrounded();
    public float VelocityY => Rigidbody2D.velocity.y;

    public void Move(float direction, Transform transform)
    {
        Rotate(direction, transform);
        MoveHorizontal(direction);
    }

    public void Jump()
    {
        if (IsGrounded == false)
            return;

        Rigidbody2D.AddForce(_jumpForce * JumpForceMultiplier * Vector2.up);
    }

    public void DrawGizmos()
    {
        Gizmos.color = new Color(16, 16, 16);
        Gizmos.DrawWireCube(_groundTransform.position, _groundPointSize);
    }

    protected sealed override void OnInit() { }

    private bool WasGrounded()
        => Physics2D.BoxCast
        (_groundTransform.position, _groundPointSize, 
        0, Vector2.down, 0, _groundMask)
        .collider != null;
}