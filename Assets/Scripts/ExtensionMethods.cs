using Cysharp.Threading.Tasks;
using UnityEngine;

public static class ExtensionMethods
{
    public static void HideWarning(this UniTask uniTask) { }

    public static void SetVelocity(this Rigidbody2D rigidbody2D, float x, float y) =>
        rigidbody2D.velocity = new Vector2(x, y);

    public static void Flip(this Transform transform, float velocityX)
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