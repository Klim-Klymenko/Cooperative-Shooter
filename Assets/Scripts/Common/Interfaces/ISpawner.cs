using UnityEngine;

namespace Common
{
    public interface ISpawner<T> where T : Object
    {
        T Spawn(string objectType);
        void Despawn(T obj);
    }
}