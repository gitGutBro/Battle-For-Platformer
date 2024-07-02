public class Wallet
{
    private uint _coins;

    public void AddCoins(int coins) => 
        _coins += (uint)coins;
}