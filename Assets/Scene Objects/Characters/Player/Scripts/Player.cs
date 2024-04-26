using UnityEngine;

public class Player : Character
{
    [SerializeField] private Purse _purse;

    [Header("Movement")]
    [SerializeField] private PlayerController _controller;



    protected sealed override void OnInit() 
    {
        _controller.Init(GetComponent<Rigidbody2D>());
    }
}