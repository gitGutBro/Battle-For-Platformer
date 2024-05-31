using System;
using UnityEngine;

public interface INotifyVelocityChanged
{
    event Action<Vector2> VelocityChanged;
}