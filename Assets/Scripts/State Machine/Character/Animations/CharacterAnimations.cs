using System;
using UnityEngine;

[Serializable]
public struct CharacterAnimations
{
    [field: SerializeField] public AnimationClip Idle { get; private set; }
    [field: SerializeField] public AnimationClip Jump { get; private set; }
    [field: SerializeField] public AnimationClip Move { get; private set; }
}