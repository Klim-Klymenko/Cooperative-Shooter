using Common;
using GameEngine.Data;
using UnityEngine;
using Zenject;

namespace Objects
{
    internal sealed class BulletInstaller : MonoInstaller
    {
        [SerializeField]
        private int _reservationAmount;
        
        [SerializeField]
        private Bullet[] _prefabs;
        
        [SerializeField]
        private Transform _poolContainer;

        [SerializeField] 
        private BulletType[] _bulletTypes;
        
        [SerializeField]
        private Transform[] _firePoints;
        
        public override void InstallBindings()
        {
            BindBulletManager();
        }
        
        private void BindBulletManager()
        {
            Pool<Bullet>[] pools = CreatePools();
            
            Container.BindInterfacesTo<BulletManager>().AsSingle().WithArguments(pools, _firePoints);
        }

        private Pool<Bullet>[] CreatePools()
        {
            return new Pool<Bullet>[]
            {
                new Pool<Bullet>(_reservationAmount, _prefabs[0], _poolContainer, _bulletTypes[0].ToString()),
                new Pool<Bullet>(_reservationAmount, _prefabs[1], _poolContainer, _bulletTypes[1].ToString())
            };
        }
    }
}