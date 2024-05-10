using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Shop.Configs
{
    [CreateAssetMenu(fileName = "ProductCatalog", menuName = "Shop/ProductCatalog")]
    public sealed class ProductCatalog : ScriptableObject
    {
        [SerializeField] 
        private ProductInfo[] _productInfos;

        public IReadOnlyList<ProductInfo> ProductInfos => _productInfos;
    }
}