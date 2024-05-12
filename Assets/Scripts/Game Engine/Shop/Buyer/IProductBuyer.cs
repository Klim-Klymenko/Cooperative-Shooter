namespace GameEngine.Shop
{
    public interface IProductBuyer
    {
        void BuyProduct(int price);
        bool CanBuyProduct(int price);
    }
}