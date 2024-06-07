using System;
using UnityEngine;

[Serializable]
public class PlayerMover : CharacterMover
{
    [SerializeField][Min(1.5f)] private float _jumpForce;
    [Space]
    [Header("Objects for interact with ground")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private CapsuleCollider2D _onGroundCollider;

    public bool IsGrounded => WasGrounded();

    public void Jump()
    {
        if (IsGrounded)
            ToJump();
    }

    private void ToJump() =>
        Rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

    private bool WasGrounded() => 
        Physics2D.OverlapCircleAll
        (_onGroundCollider.transform.position, _onGroundCollider.size.x, _groundMask)
        .Length > 0;
}