using UnityEngine;

public struct Fliper
{
    public readonly void Flip(float velocityX, Transform transform)
    {
        if (velocityX > 0)
        {
            Quaternion forwardRotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = forwardRotation;
        }
        else if (velocityX < 0)
        {
            Quaternion backwardRotation = Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = backwardRotation;
        }
    }
}