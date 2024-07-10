using UnityEngine;

public class Heart : Item
{
    [SerializeField][Range(1, 10)] private int _heal;

    protected override void OnUse(IItemPicker itemPicker) =>
        itemPicker.HealthBar.Health.Increase(_heal);
}