using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [Header("Moving Settings")]
    [SerializeField] private Mover _mover;

    [SerializeField] private Animation _animation;


    private void Awake() =>
        Init();

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void Update()
    {
        _mover.JumpByKey();
    }

    private void FixedUpdate()
    {
        _mover.Move(transform);
    }

    private void Init()
    {
        _mover.Init(GetComponent<Rigidbody2D>());
    }
}