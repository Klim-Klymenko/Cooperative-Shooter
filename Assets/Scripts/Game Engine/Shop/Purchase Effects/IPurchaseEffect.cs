using GameEngine.Shop.Configs;

namespace GameEngine.Shop
{
    public interface IPurchaseEffect
    {
        void Invoke(ProductInfo productInfo);
    }
}