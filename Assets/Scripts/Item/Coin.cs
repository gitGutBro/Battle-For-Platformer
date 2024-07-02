public class Coin : Item
{
    private const int Count = 1;

    protected override void OnUse(IItemPicker itemPicker) => 
        itemPicker.Wallet.AddCoins(Count);
}