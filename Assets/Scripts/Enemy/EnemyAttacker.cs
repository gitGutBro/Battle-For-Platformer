using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker
{
    public bool IsTargetFound { get; private set; }

    public void ChangeTargetState(bool state) =>
        IsTargetFound = state;
}