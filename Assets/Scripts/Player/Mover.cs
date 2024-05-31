using System;
using UnityEngine;

[Serializable]
public class Mover : INotifyVelocityChanged
{
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode JumpKey = KeyCode.W;

    [Header("Move Multipliers")]
    [SerializeField][Min(1.5f)] private float _jumpForce;
    [SerializeField][Min(1.5f)] private float _speed;
    [Space]
    [Header("Objects for interact with ground")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private CapsuleCollider2D _onGroundCollider;

    private Fliper _fliper;
    private Rigidbody2D _rigidbody;

    public event Action<Vector2> VelocityChanged;

    public bool IsGrounded => WasGrounded();
    
    public void Init(Rigidbody2D rigidbody)
    {
        _fliper = new();
        _rigidbody = rigidbody;
    }

    public void Move(Transform transform)
    {
        float direction = Input.GetAxis(Horizontal);

        ToMove(direction * _speed, _rigidbody.velocity.y);
        VelocityChanged?.Invoke(_rigidbody.velocity);
        _fliper.Flip(direction, transform);
    }

    public void JumpByKey()
    {
        if (IsGrounded && Input.GetKeyDown(JumpKey))
            ToJump();
    }

    private void ToMove(float x, float y) =>
        _rigidbody.velocity = new Vector2(x, y);

    private void ToJump() =>
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

    private bool WasGrounded() =>
        Physics2D.OverlapCircleAll
        (_onGroundCollider.transform.position, _onGroundCollider.size.y, _groundMask)
        .Length > 0;
}