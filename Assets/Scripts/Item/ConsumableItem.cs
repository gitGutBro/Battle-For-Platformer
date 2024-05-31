using UnityEngine;

public abstract class ConsumableItem : MonoBehaviour
{
    protected virtual void Use() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Use();
            gameObject.SetActive(false);
        }
    }
}