using System;
using UnityEngine;

[Serializable]
public class CharacterMover
{
    [SerializeField][Min(1.5f)] private float _speed;

    private Fliper _fliper;
    private Transform _transform;

    protected Rigidbody2D Rigidbody { get; private set; }

    public void Move(float direction)
    {
        ToMove(direction * _speed, Rigidbody.velocity.y);

        _fliper.Flip(direction, _transform);
    }

    public void Init(Rigidbody2D rigidbody)
    {
        _fliper = new();
        Rigidbody = rigidbody;
        _transform = Rigidbody.transform;
    }

    private void ToMove(float x, float y) =>
        Rigidbody.velocity = new Vector2(x, y);
}