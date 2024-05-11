using UnityEngine;

namespace GameEngine.Interfaces
{
    public interface IBulletSpawner
    {
        Transform Spawn(string objectType);
        void Despawn(Transform bulletTransform);
    }
}