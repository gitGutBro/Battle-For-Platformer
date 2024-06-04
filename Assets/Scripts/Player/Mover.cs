using System;
using UnityEngine;

[Serializable]
public class Mover
{
    [Header("Move Multipliers")]
    [SerializeField][Min(1.5f)] private float _jumpForce;
    [SerializeField][Min(1.5f)] private float _speed;
    [Space]
    [Header("Objects for interact with ground")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private CapsuleCollider2D _onGroundCollider;

    private Fliper _fliper;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    
    public float Velocity => _rigidbody.velocity.magnitude;
    public bool IsGrounded { get; private set; }

    public void Init(Rigidbody2D rigidbody)
    {
        _fliper = new();
        _rigidbody = rigidbody;
        _transform = _rigidbody.transform;
    }

    public void Update()
    {
        if (_rigidbody.velocity.y <= 0.8)
            IsGrounded = WasGrounded();
    }

    public void Move(float direction)
    {
        ToMove(direction * _speed, _rigidbody.velocity.y);

        _fliper.Flip(direction, _transform);
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            ToJump();
            IsGrounded = false;
        }
    }

    private void ToMove(float x, float y) =>
        _rigidbody.velocity = new Vector2(x, y);

    private void ToJump() =>
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

    private bool WasGrounded() => 
        Physics2D.OverlapCircleAll
        (_onGroundCollider.transform.position, _onGroundCollider.size.x, _groundMask)
        .Length > 0;
}