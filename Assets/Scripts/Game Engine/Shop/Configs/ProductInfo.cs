using UnityEngine;

namespace GameEngine.Shop.Configs
{
    [CreateAssetMenu(fileName = "ProductInfo", menuName = "Shop/ProductInfo")]
    public sealed class ProductInfo : ScriptableObject
    {
        [field: SerializeField]
        public Sprite Icon { get; private set; }
        
        [field: SerializeField]
        public Sprite Frame { get; private set; }
        
        [field: SerializeField]
        public int Price { get; private set; }
        
        [field: SerializeField]
        public int Quantity { get; private set; }
    }
}