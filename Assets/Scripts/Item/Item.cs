using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public void Use(IItemPicker itemPicker)
    {
        OnUse(itemPicker);
        gameObject.SetActive(false);
    }

    protected virtual void OnUse(IItemPicker itemPicker) { }
}