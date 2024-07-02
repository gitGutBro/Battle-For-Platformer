using System;
using UnityEngine;

[Serializable]
public class PlayerMover
{
    [SerializeField][Min(1.5f)] private float _speed;
    [SerializeField][Min(1.5f)] private float _jumpForce;
    [Space]
    [Header("Objects for interact with ground")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private CapsuleCollider2D _onGroundCollider;

    private Rigidbody2D _rigidbody2D;

    public bool IsGrounded => WasGrounded();

    public void Jump()
    {
        if (IsGrounded)
            JumpTo();
    }

    public void Move(float direction)
    {
        _rigidbody2D.SetVelocity(direction * _speed, _rigidbody2D.velocity.y);
        _rigidbody2D.transform.Flip(direction);
    }

    public void Init(Rigidbody2D rigidbody2D) => 
        _rigidbody2D = rigidbody2D;

    private void JumpTo() =>
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

    private bool WasGrounded() => 
        Physics2D.OverlapCircleAll
        (_onGroundCollider.transform.position, _onGroundCollider.size.x, _groundMask)
        .Length > 0;
}