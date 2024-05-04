using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Extensions;
using Common;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;
using UnityEngine;

namespace Objects
{
    [UsedImplicitly]
    internal sealed class BulletManager : ISpawner<Transform>, IFinishGameListener
    {
        private readonly List<Bullet> _activeBullets = new();
        
        private readonly Pool<Bullet>[] _pools;
        private readonly Transform[] _firePoints;
        private readonly GameCycleManager _gameCycleManager;

        internal BulletManager(Pool<Bullet>[] pools, Transform[] firePoints, GameCycleManager gameCycleManager)
        {
            _pools = pools;
            _firePoints = firePoints;
            _gameCycleManager = gameCycleManager;
        }

        Transform ISpawner<Transform>.Spawn(string objectType)
        {
            int index = FindPoolIndex(objectType);
            
            Bullet bullet = _pools[index].Get();
            Transform bulletTransform = bullet.transform;

            Transform firePoint = _firePoints[index];
            
            bulletTransform.position = firePoint.position;
            bulletTransform.forward = firePoint.forward;
            
            bullet.Compose();

            IAtomicObservable destroyObservable = bullet.GetObservable(LiveableAPI.DeathObservable);
           
            destroyObservable.Subscribe(() => Despawn(bulletTransform));
            
            if (!_gameCycleManager.ContainsListener(bullet))
                _gameCycleManager.AddListener(bullet);
            
            _activeBullets.Add(bullet);
            
            return bulletTransform;
        }
        
        public void Despawn(Transform obj)
        {
            Bullet bullet = obj.GetComponent<Bullet>();
            
            bullet.OnFinish();
            _gameCycleManager.RemoveListener(bullet);

            _activeBullets.Remove(bullet);

            int index = FindPoolIndex(bullet.Types());
            _pools[index].Put(bullet);
        }

        private int FindPoolIndex(params string[] objectTypes)
        {
            for (int i = 0; i < objectTypes.Length; i++)
            {
                for (int j = 0; j < _pools.Length; j++)
                {
                    if (objectTypes[i] == _pools[j].ObjectType)
                        return j;
                }
            }
            
            throw new Exception("Pool not found");
        }
        
        void IFinishGameListener.OnFinish()
        {
            for (int i = 0; i < _activeBullets.Count; i++)
            {
                Bullet bullet = _activeBullets[i];
                
                if (bullet == null) continue;
                Despawn(bullet.transform);
            }
            
            _activeBullets.Clear();
        }
    }
}