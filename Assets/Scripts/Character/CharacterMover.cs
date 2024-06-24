using System;
using UnityEngine;

[Serializable]
public class CharacterMover
{
    [SerializeField][Min(1.5f)] private float _speed;

    protected float Speed => _speed;
    protected Fliper Fliper { get; private set; }
    protected Rigidbody2D Rigidbody { get; private set; }
    protected Transform Transform { get; private set; }

    protected void ToMove(float x, float y) =>
        Rigidbody.velocity = new Vector2(x, y);

    protected virtual void OnInit() { }

    public void Init(Rigidbody2D rigidbody)
    {
        Fliper = new();
        Rigidbody = rigidbody;
        Transform = Rigidbody.transform;

        OnInit();
    }
}