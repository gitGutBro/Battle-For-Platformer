using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterMover
{
    private readonly Quaternion ForwardRotation = Quaternion.Euler(0f, 0f, 0f);
    private readonly Quaternion BackwardRotation = Quaternion.Euler(0f, 180f, 0f);

    [SerializeField] private float _speed; 

    protected Rigidbody2D Rigidbody2D { get; private set; }

    public void Init(Rigidbody2D rigidbody2D)
    {
        Rigidbody2D = rigidbody2D;

        OnInit();
    }

    public void ResetVelocityX() =>
        Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);

    protected void MoveHorizontal(float direction) =>
        Rigidbody2D.velocity = new Vector2(direction * _speed, Rigidbody2D.velocity.y);

    protected void Rotate(float direction, Transform transform)
    {
        if (direction < 0)
            transform.rotation = ForwardRotation;
        else if (direction > 0)
            transform.rotation = BackwardRotation;
    }

    protected virtual void OnInit() { }
}